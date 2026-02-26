import { Component, inject, Input, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { noop, Observable, Observer, of } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';
import { LookupsService as ApiLookupsService } from '../../../api/lookups/lookups.service';
import { config } from '../../../config';
import { CitySearchResponseDto } from '../../../model';
import { iCity, iCountry, iLookupData, iProvince } from '../../interfaces/lookup-data.interface';
import { LookupsStore } from '../../store/lookups/lookups.store';
import { COUNTRIES_ADDRESS } from '../address/country-list';
import { POSTAL_CODE, ZIP_CODE } from '../regex.constants';

@Component({
  selector: 'app-restitution-address',
  templateUrl: './address.component.html',
  standalone: false
})
export class RestitutionAddressComponent implements OnInit {
  countryList: iCountry[] = config.preferred_countries;
  preferred_countries: iCountry[] = config.preferred_countries;
  postalRegex = POSTAL_CODE;
  zipRegex = ZIP_CODE;

  provinceList: iProvince[];
  provinceType: string;
  postalCodeType: string;
  postalCodeSample: string;

  cityList: iCity[] = [];
  search: string;
  citySuggestions$: Observable<iCity[]>;
  errorMessage: string;

  selectedCountry: iCountry;
  selectedProvince: iProvince;

  isProvinceDisabled: boolean = false;
  isCityDisabled: boolean = false;

  @Input() group = UntypedFormGroup;
  @Input() showChildrenAsRequired: boolean = true;
  @Input() isDisabled: boolean = false;
  lookupData: iLookupData = {
    countries: [],
    provinces: [],
    cities: []
  };
  private readonly lookupsStore = inject(LookupsStore);
  private readonly apiLookupsService = inject(ApiLookupsService);

  constructor() {
    let canada = COUNTRIES_ADDRESS.filter((c) => c.name.toLowerCase() == 'canada')[0];
    this.provinceType = canada.areaType;
    this.postalCodeType = canada.postalCodeName;
    this.postalCodeSample = canada.postalCodeSample;
  }

  ngOnInit() {
    //city search
    this.citySuggestions$ = new Observable((observer: Observer<string>) => {
      observer.next(this.group['controls']['city'].value.toString());
    }).pipe(
      switchMap((query: string) => {
        if (query) {
          let countryVal = this.group['controls']['country'].value.toString();
          let provinceVal = this.group['controls']['province'].value.toString();
          let searchVal = this.group['controls']['city'].value.toString();
          return this.apiLookupsService
            .getApiLookupsCitiesSearch('application/json', {
              country: countryVal,
              province: provinceVal,
              searchVal,
              limit: 15
            })
            .pipe(
              map((data: CitySearchResponseDto) => {
                if (data && data.cityCollection) {
                  const cityCollection = [...data.cityCollection] as iCity[];
                  cityCollection.sort((a, b) => a.vsd_name.localeCompare(b.vsd_name));
                  return cityCollection;
                } else return [];
              }),
              tap(
                () => noop,
                (err) => {
                  // in case of http error
                  this.errorMessage = (err && err.message) || 'Something goes wrong';
                }
              )
            );
        }
        return of([]);
      })
    );

    this.countryList = config.preferred_countries;
    this.provinceList = [];

    let promise_array = [];
    if (!this.lookupData.countries || this.lookupData.countries.length == 0) {
      promise_array.push(
        new Promise<void>(async (resolve) => {
          if (!this.lookupsStore.isCountriesLoaded()) {
            await this.lookupsStore.loadCountries();
          }

          this.lookupData.countries = [...this.lookupsStore.countries()];

          if (this.lookupData.countries) {
            this.lookupData.countries.sort(function (a, b) {
              return (a.vsd_name ?? '').localeCompare(b.vsd_name ?? '');
            });
          }

          resolve();
        })
      );
    }

    if (!this.lookupData.provinces || this.lookupData.provinces.length == 0) {
      promise_array.push(
        new Promise<void>(async (resolve) => {
          if (!this.lookupsStore.isProvincesLoaded()) {
            await this.lookupsStore.loadProvinces();
          }

          this.lookupData.provinces = [...this.lookupsStore.provinces()];

          if (this.lookupData.provinces) {
            this.lookupData.provinces.sort(function (a, b) {
              return (a.vsd_name ?? '').localeCompare(b.vsd_name ?? '');
            });
          }

          resolve();
        })
      );
    }

    if (!this.lookupData.cities || this.lookupData.cities.length == 0) {
      promise_array.push(
        new Promise<void>(async (resolve) => {
          if (!this.lookupsStore.isCitiesLoaded()) {
            await this.lookupsStore.loadCities();
          }

          this.lookupData.cities = [...this.lookupsStore.cities()];

          if (this.lookupData.cities) {
            this.lookupData.cities.sort(function (a, b) {
              return (a.vsd_name ?? '').localeCompare(b.vsd_name ?? '');
            });
          }

          resolve();
        })
      );
    }

    Promise.all(promise_array).then((res) => {
      this.setupForm();
    });
  }

  setupForm() {
    if (this.showChildrenAsRequired === undefined) {
      this.showChildrenAsRequired = true;
    }

    let pref_countries = this.lookupData.countries.filter(
      (c) => config.preferred_countries.findIndex((pc) => pc.vsd_countryid == c.vsd_countryid) >= 0
    );
    let remaining_countries = this.lookupData.countries.filter(
      (c) => config.preferred_countries.findIndex((pc) => pc.vsd_countryid == c.vsd_countryid) < 0
    );

    pref_countries.sort(function (a, b) {
      return (
        config.preferred_countries.findIndex((c) => c.vsd_countryid == a.vsd_countryid) -
        config.preferred_countries.findIndex((c) => c.vsd_countryid == b.vsd_countryid)
      );
    });
    if (!this.alreadyHasOtherOption(pref_countries) && !this.alreadyHasOtherOption(remaining_countries))
      pref_countries.unshift(config.other_country);

    remaining_countries.sort((a, b) => a.vsd_name.localeCompare(b.vsd_name));

    this.countryList = pref_countries.concat(remaining_countries);
    this.cityList = this.lookupData.cities;
    this.cityList.sort((a, b) => a.vsd_name.localeCompare(b.vsd_name));
    let other_city_index = this.getOtherIndex(this.cityList);
    if (other_city_index < 0) {
      this.cityList.unshift(config.other_city);
    } else {
      let other_city = this.cityList.splice(other_city_index, 1)[0];
      this.cityList.unshift(other_city);
    }

    let canada = COUNTRIES_ADDRESS.filter((c) => c.name.toLowerCase() == 'canada')[0];
    this.provinceType = canada.areaType;
    this.postalCodeType = canada.postalCodeName;
    this.postalCodeSample = canada.postalCodeSample;

    let countryVal = this.group['controls']['country'].value.toString();
    this.selectedCountry = this.lookupData.countries.filter(
      (c) => c.vsd_name.toLowerCase() == countryVal.toLowerCase()
    )[0];
    if (countryVal === 'Other') this.selectedCountry = { vsd_name: 'Other', vsd_countryid: '123' };
    if (!this.selectedCountry) {
      this.selectedCountry = this.lookupData.countries.filter((p) => p.vsd_name.toLowerCase() === 'canada')[0];
    }

    if (this.selectedCountry) {
      this.provinceList = this.lookupData.provinces.filter(
        (p) => p._vsd_countryid_value === this.selectedCountry.vsd_countryid
      );
      this.provinceList.sort((a, b) => a.vsd_name.localeCompare(b.vsd_name));
      let other_province_index = this.getOtherIndex(this.provinceList);
      if (other_province_index < 0) {
        this.provinceList.unshift(config.other_province);
      } else {
        let other_province = this.provinceList.splice(other_province_index, 1)[0];
        this.provinceList.unshift(other_province);
      }
    }

    if (this.selectedCountry) {
      this.setProvinceAndPostalType(this.selectedCountry.vsd_name);
    }

    let provinceVal = this.group['controls']['province'].value.toString();
    this.selectedProvince = this.lookupData.provinces.filter(
      (c) => c.vsd_name.toLowerCase() == provinceVal.toLowerCase()
    )[0];
    if (this.selectedProvince && this.selectedProvince.vsd_name != 'British Columbia') this.updateCityList();
    else this.setCityValidators();
    this.setProvinceValidators();
  }

  isSubFieldValid(field: string, disabled: boolean) {
    if (disabled === true) return true;
    let formField = this.group['controls'][field];
    if (formField == null) return true;

    return formField.valid || !formField.touched;
  }

  onCountryChange(event) {
    let provinceControl = this.group['controls']['province'] as UntypedFormControl;
    provinceControl.patchValue('');
    this.selectedProvince = { vsd_name: '', _vsd_countryid_value: '', vsd_code: '', vsd_provinceid: '' };
    let cityControl = this.group['controls']['city'] as UntypedFormControl;
    cityControl.patchValue('');

    let selection = event.target.value.toLowerCase();
    this.selectedCountry = this.lookupData.countries.filter((c) => c.vsd_name.toLowerCase() == selection)[0];
    if (this.selectedCountry) {
      this.provinceList = this.lookupData.provinces.filter(
        (p) => p._vsd_countryid_value === this.selectedCountry.vsd_countryid
      );
      if (this.provinceList) {
        this.provinceList.sort((a, b) => a.vsd_name.localeCompare(b.vsd_name));
      }
      let other_province_index = this.getOtherIndex(this.provinceList);
      if (other_province_index < 0) {
        this.provinceList.unshift(config.other_province);
      } else {
        let other_province = this.provinceList.splice(other_province_index, 1)[0];
        this.provinceList.unshift(other_province);
      }

      provinceControl.patchValue('');
      this.setProvinceValidators();

      let postalControl = this.group['controls']['postalCode'] as UntypedFormControl;
      postalControl.patchValue('');

      this.setProvinceAndPostalType(this.selectedCountry.vsd_name);
      this.updateCityList();
    } else {
      this.provinceList = [config.other_province];
      this.setProvinceAndPostalType('');
      this.cityList = [config.other_city];
      this.setCityValidators();
    }
  }

  onProvinceChange(event) {
    let cityControl = this.group['controls']['city'] as UntypedFormControl;
    cityControl.patchValue('');
    let selection = event.target.value.toLowerCase();
    this.selectedProvince = this.lookupData.provinces.filter((c) => c.vsd_name.toLowerCase() == selection)[0];
    this.updateCityList();
  }

  updateCityList() {
    if (
      this.selectedProvince &&
      this.selectedCountry &&
      this.selectedCountry.vsd_countryid &&
      this.selectedProvince.vsd_provinceid
    ) {
      this.apiLookupsService
        .getApiLookupsCountryCountryIdProvinceProvinceIdCities(
          this.selectedCountry.vsd_countryid,
          this.selectedProvince.vsd_provinceid,
          'application/json'
        )
        .subscribe((city_res) => {
          if (city_res.value) {
            this.cityList = city_res.value as iCity[];
            if (this.cityList) {
              this.cityList.sort((a, b) => a.vsd_name.localeCompare(b.vsd_name));
            }
            let other_city_index = this.getOtherIndex(this.cityList);
            if (other_city_index < 0) {
              this.cityList.unshift(config.other_city);
            } else {
              let other_city = this.cityList.splice(other_city_index, 1)[0];
              this.cityList.unshift(other_city);
            }
          } else {
            this.cityList = [config.other_city];
          }
          this.setCityValidators();
        });
    } else if (this.provinceList.length == 1 && this.selectedCountry && this.selectedCountry.vsd_countryid) {
      this.apiLookupsService
        .getApiLookupsCountryCountryCities(this.selectedCountry.vsd_countryid, 'application/json')
        .subscribe((city_res) => {
          if (city_res.value) {
            this.cityList = city_res.value as iCity[];
            if (this.cityList) {
              this.cityList.sort((a, b) => a.vsd_name.localeCompare(b.vsd_name));
            }
            let other_city_index = this.getOtherIndex(this.cityList);
            if (other_city_index < 0) {
              this.cityList.unshift(config.other_city);
            } else {
              let other_city = this.cityList.splice(other_city_index, 1)[0];
              this.cityList.unshift(other_city);
            }
          } else {
            this.cityList = [config.other_city];
          }
          this.setCityValidators();
        });
    } else {
      this.cityList = [config.other_city];
      this.setCityValidators();
    }
  }

  setProvinceAndPostalType(country: string) {
    let postalControl = this.group['controls']['postalCode'] as UntypedFormControl;
    if (country.toLowerCase() === 'canada') {
      if (this.showChildrenAsRequired) {
        postalControl.setValidators([Validators.required, Validators.pattern(this.postalRegex)]);
      } else {
        postalControl.setValidators([Validators.pattern(this.postalRegex)]);
      }
      let canada = COUNTRIES_ADDRESS.filter((c) => c.name.toLowerCase() == 'canada')[0];
      this.provinceType = canada.areaType;
      this.postalCodeType = canada.postalCodeName;
      this.postalCodeSample = canada.postalCodeSample;
    } else if (country.toLowerCase() === 'united states of america') {
      postalControl.setValidators([Validators.pattern(this.zipRegex)]);
      let usa = COUNTRIES_ADDRESS.filter((c) => c.name.toLowerCase() == 'united states of america')[0];
      this.provinceType = usa.areaType;
      this.postalCodeType = usa.postalCodeName;
      this.postalCodeSample = usa.postalCodeSample;
    } else {
      postalControl.clearValidators();
      this.provinceType = 'Province/State';
      this.postalCodeType = 'Postal/ZIP Code';
      this.postalCodeSample = '';
    }
    postalControl.updateValueAndValidity();
  }

  setProvinceValidators() {
    let provinceControl = this.group['controls']['province'] as UntypedFormControl;
    if (this.provinceList.length == 0) {
      provinceControl.setErrors(null);
      provinceControl.disable();
      this.isProvinceDisabled = true;
    } else {
      provinceControl.enable();
      this.isProvinceDisabled = false;
    }
  }

  setCityValidators() {
    let provinceControl = this.group['controls']['province'] as UntypedFormControl;
    let cityControl = this.group['controls']['city'] as UntypedFormControl;

    if ((provinceControl.valid && this.cityList.length == 0) || provinceControl.disabled) {
      cityControl.setErrors(null);
      cityControl.disable();
      this.isCityDisabled = true;
    } else {
      cityControl.enable();
      this.isCityDisabled = false;
    }
  }

  alreadyHasOtherOption(list: any) {
    return list.findIndex((o) => o.vsd_name == 'Other') >= 0;
  }

  getOtherIndex(list: any) {
    return list.findIndex((o) => o.vsd_name == 'Other');
  }
}
