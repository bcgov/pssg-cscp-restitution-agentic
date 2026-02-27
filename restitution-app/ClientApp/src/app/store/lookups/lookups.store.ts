import { inject } from '@angular/core';
import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { firstValueFrom } from 'rxjs';
import { LookupsService as ApiLookupsService } from '../../../api/lookups/lookups.service';
import { CityLookupDto, CountryLookupDto, LookupItemDto, ProvinceLookupDto } from '../../../model';

export interface LookupsStoreState {
  countries: CountryLookupDto[];
  isCountriesLoaded: boolean;
  isCountriesLoading: boolean;
  countriesError: string | null;
  provinces: ProvinceLookupDto[];
  isProvincesLoaded: boolean;
  isProvincesLoading: boolean;
  provincesError: string | null;
  cities: CityLookupDto[];
  isCitiesLoaded: boolean;
  isCitiesLoading: boolean;
  citiesError: string | null;
  courts: LookupItemDto[];
  isCourtsLoaded: boolean;
  isCourtsLoading: boolean;
  courtsError: string | null;
}

const initialState: LookupsStoreState = {
  countries: [],
  isCountriesLoaded: false,
  isCountriesLoading: false,
  countriesError: null,
  provinces: [],
  isProvincesLoaded: false,
  isProvincesLoading: false,
  provincesError: null,
  cities: [],
  isCitiesLoaded: false,
  isCitiesLoading: false,
  citiesError: null,
  courts: [],
  isCourtsLoaded: false,
  isCourtsLoading: false,
  courtsError: null
};

export const LookupsStore = signalStore(
  { providedIn: 'root' },
  withState(initialState),
  withMethods((store) => {
    const apiLookupsService = inject(ApiLookupsService);

    return {
      setCountriesLoading(isCountriesLoading: boolean) {
        patchState(store, { isCountriesLoading });
      },
      setCountries(countries: CountryLookupDto[]) {
        patchState(store, {
          countries,
          isCountriesLoaded: true,
          isCountriesLoading: false,
          countriesError: null
        });
      },
      setCountriesError(countriesError: string) {
        patchState(store, {
          countriesError,
          isCountriesLoading: false
        });
      },
      setProvincesLoading(isProvincesLoading: boolean) {
        patchState(store, { isProvincesLoading });
      },
      setProvinces(provinces: ProvinceLookupDto[]) {
        patchState(store, {
          provinces,
          isProvincesLoaded: true,
          isProvincesLoading: false,
          provincesError: null
        });
      },
      setProvincesError(provincesError: string) {
        patchState(store, {
          provincesError,
          isProvincesLoading: false
        });
      },
      setCitiesLoading(isCitiesLoading: boolean) {
        patchState(store, { isCitiesLoading });
      },
      setCities(cities: CityLookupDto[]) {
        patchState(store, {
          cities,
          isCitiesLoaded: true,
          isCitiesLoading: false,
          citiesError: null
        });
      },
      setCitiesError(citiesError: string) {
        patchState(store, {
          citiesError,
          isCitiesLoading: false
        });
      },
      setCourtsLoading(isCourtsLoading: boolean) {
        patchState(store, { isCourtsLoading });
      },
      setCourts(courts: LookupItemDto[]) {
        patchState(store, {
          courts,
          isCourtsLoaded: true,
          isCourtsLoading: false,
          courtsError: null
        });
      },
      setCourtsError(courtsError: string) {
        patchState(store, {
          courtsError,
          isCourtsLoading: false
        });
      },
      async loadCountries(): Promise<void> {
        if (store.isCountriesLoaded() || store.isCountriesLoading()) {
          return;
        }

        patchState(store, { isCountriesLoading: true });

        try {
          const response = await firstValueFrom(apiLookupsService.getApiLookupsCountries('application/json'));
          const countries = [...(response.value ?? [])].sort((a, b) => (a.name ?? '').localeCompare(b.name ?? ''));

          patchState(store, {
            countries,
            isCountriesLoaded: true,
            isCountriesLoading: false,
            countriesError: null
          });
        } catch (error) {
          const errorMessage = error instanceof Error ? error.message : 'Failed to fetch countries lookup data.';
          patchState(store, {
            countriesError: errorMessage,
            isCountriesLoading: false
          });
        }
      },
      async loadProvinces(): Promise<void> {
        if (store.isProvincesLoaded() || store.isProvincesLoading()) {
          return;
        }

        patchState(store, { isProvincesLoading: true });

        try {
          const response = await firstValueFrom(apiLookupsService.getApiLookupsProvinces('application/json'));
          const provinces = [...(response.value ?? [])].sort((a, b) => (a.name ?? '').localeCompare(b.name ?? ''));

          patchState(store, {
            provinces: provinces as ProvinceLookupDto[],
            isProvincesLoaded: true,
            isProvincesLoading: false,
            provincesError: null
          });
        } catch (error) {
          const errorMessage = error instanceof Error ? error.message : 'Failed to fetch provinces lookup data.';
          patchState(store, {
            provincesError: errorMessage,
            isProvincesLoading: false
          });
        }
      },
      async loadCities(): Promise<void> {
        if (store.isCitiesLoaded() || store.isCitiesLoading()) {
          return;
        }

        patchState(store, { isCitiesLoading: true });

        try {
          const response = await firstValueFrom(apiLookupsService.getApiLookupsCities('application/json'));
          const cities = [...(response.value ?? [])].sort((a, b) => (a.name ?? '').localeCompare(b.name ?? ''));

          patchState(store, {
            cities: cities as CityLookupDto[],
            isCitiesLoaded: true,
            isCitiesLoading: false,
            citiesError: null
          });
        } catch (error) {
          const errorMessage = error instanceof Error ? error.message : 'Failed to fetch cities lookup data.';
          patchState(store, {
            citiesError: errorMessage,
            isCitiesLoading: false
          });
        }
      },
      async loadCourts(): Promise<void> {
        if (store.isCourtsLoaded() || store.isCourtsLoading()) {
          return;
        }

        patchState(store, { isCourtsLoading: true });

        try {
          const response = await firstValueFrom(apiLookupsService.getApiLookupsCourts('application/json'));
          const courts = [...(response.value ?? [])].sort((a, b) => (a.name ?? '').localeCompare(b.name ?? ''));

          patchState(store, {
            courts: courts as LookupItemDto[],
            isCourtsLoaded: true,
            isCourtsLoading: false,
            courtsError: null
          });
        } catch (error) {
          const errorMessage = error instanceof Error ? error.message : 'Failed to fetch courts lookup data.';
          patchState(store, {
            courtsError: errorMessage,
            isCourtsLoading: false
          });
        }
      }
    };
  })
);
