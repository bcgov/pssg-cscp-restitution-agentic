import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormGroup,
  UntypedFormArray,
  UntypedFormBuilder,
  UntypedFormGroup
} from '@angular/forms';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatStepper } from '@angular/material/stepper';
import { ActivatedRoute, Router } from '@angular/router';
import { RestitutionsService } from '../../api/restitutions/restitutions.service';
import { config } from '../../config';
import { ApplicationDto, CreateRestitutionCaseRequestDto, ParticipantDto } from '../../model';
import { iLookupData } from '../interfaces/lookup-data.interface';
import { iRestitutionApplication } from '../interfaces/restitution.interface';
import { JusticeApplicationDataService } from '../services/justice-application-data.service';
import { LookupService } from '../services/lookup.service';
import { StateService } from '../services/state.service';
import { CancelDialog } from '../shared/dialogs/cancel/cancel.dialog';
import { ApplicationType, CRMBoolean, IOptionSetVal, MY_FORMATS, ResitutionForm } from '../shared/enums-list';
import { FormBase } from '../shared/form-base';
import { RestitutionInfoHelper } from '../shared/restitution/restitution-information/restitution-information.helper';
import { ServiceNotAvailableComponent } from '../shared/service-not-available.component';
import { convertRestitutionToCRM } from './restitution.to.crm';

export enum RESTITUTION_PAGES {
  OVERVIEW,
  RESTITUTION_INFORMATION,
  REVIEW,
  CONFIRMATION
}

@Component({
  selector: 'app-restitution-application',
  templateUrl: './restitution-application.component.html',
  styleUrls: ['./restitution-application.component.scss'],
  providers: [
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
    { provide: STEPPER_GLOBAL_OPTIONS, useValue: { showError: true } }
  ],
  standalone: false
})
export class RestitutionApplicationComponent extends FormBase implements OnInit {
  restitutionsService = inject(RestitutionsService);

  @ViewChild('stepper', { static: false }) restitutionStepper: MatStepper;
  FORM_TYPE: IOptionSetVal = { val: -1, name: '' };
  ApplicationType = ApplicationType;
  isIE: boolean = false;
  didLoad: boolean = false;
  submitting: boolean = false;
  public showPrintView: boolean = false;

  PAGES = RESTITUTION_PAGES;

  lookupData: iLookupData = {
    countries: [],
    provinces: [],
    cities: []
  };

  courtList: string[] = [];

  restitutionInfoHelper = new RestitutionInfoHelper();

  constructor(
    private justiceDataService: JusticeApplicationDataService,
    public fb: UntypedFormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public snackBar: MatSnackBar,
    private matDialog: MatDialog,
    public state: StateService,
    public lookupService: LookupService
  ) {
    super();
  }

  ngOnInit() {
    var ua = window.navigator.userAgent;
    this.isIE = /MSIE|Trident/.test(ua);
    let form_type = this.route.snapshot.data['formType'];
    if (form_type) {
      this.FORM_TYPE = form_type;
    }

    if (this.state.cloning) {
      this.form = this.cloneForm(this.state.data);
      this.state.cloning = false;
    } else {
      this.form = this.buildApplicationForm();
    }

    let promise_array = [];

    promise_array.push(
      new Promise<void>((resolve, reject) => {
        this.lookupService.getCountries().subscribe(
          (res) => {
            this.lookupData.countries = res.value;
            if (this.lookupData.countries) {
              this.lookupData.countries.sort((a, b) => a.vsd_name.localeCompare(b.vsd_name));
            }
            resolve();
          },
          (error) => {
            reject(error);
          }
        );
      })
    );

    promise_array.push(
      new Promise<void>((resolve, reject) => {
        this.lookupService.getProvinces().subscribe(
          (res) => {
            this.lookupData.provinces = res.value;
            if (this.lookupData.provinces) {
              this.lookupData.provinces.sort((a, b) => a.vsd_name.localeCompare(b.vsd_name));
            }
            resolve();
          },
          (error) => {
            reject(error);
          }
        );
      })
    );

    promise_array.push(
      new Promise<void>((resolve, reject) => {
        this.lookupService.getCitiesByProvince(config.canada_crm_id, config.bc_crm_id).subscribe(
          (res) => {
            this.lookupData.cities = res.value;
            if (this.lookupData.cities) {
              this.lookupData.cities.sort((a, b) => a.vsd_name.localeCompare(b.vsd_name));
            }
            resolve();
          },
          (error) => {
            reject(error);
          }
        );
      })
    );

    Promise.all(promise_array)
      .then((res) => {
        this.didLoad = true;
      })
      .catch((err) => {
        this.snackBar.openFromComponent(ServiceNotAvailableComponent, {
          panelClass: ['red-snackbar'],
          horizontalPosition: 'center',
          verticalPosition: 'top'
        });
      });

    this.form.valueChanges.subscribe((val) => {
      this.showValidationMessage = this.hasInvalidTouchedControls(this.form);
    });
  }

  // recursively checks if all required fields are filled out touched
  // this is used to determine if the validation message should be shown
  protected hasInvalidTouchedControls(control: AbstractControl): boolean {
    if (control instanceof FormGroup) {
      // check all controls in the FormGroup
      return Object.keys(control.controls).some((key) => this.hasInvalidTouchedControls(control.get(key)));
    } else if (control instanceof FormArray) {
      // check all controls in the FormArray
      return control.controls.some((ctrl) => this.hasInvalidTouchedControls(ctrl));
    } else {
      // it's a FormControl - check if it's invalid and touched
      return control.invalid && control.touched;
    }
  }

  cloneForm(data, FORM: IOptionSetVal = this.FORM_TYPE): UntypedFormGroup {
    let clonedForm: UntypedFormGroup = data.form;

    let courtFiles = clonedForm.get('restitutionInformation.courtFiles') as UntypedFormArray;
    while (courtFiles.length > 0) {
      courtFiles.removeAt(0);
    }

    courtFiles.push(this.restitutionInfoHelper.createCourtFile(this.fb, FORM));

    let documents = clonedForm.get('restitutionInformation.documents') as UntypedFormArray;
    while (documents.length > 0) {
      documents.removeAt(0);
    }

    clonedForm.get('restitutionInformation.declaredAndSigned').patchValue('');
    clonedForm.get('restitutionInformation.declaredAndSigned').markAsUntouched();
    clonedForm.get('restitutionInformation.signature').patchValue('');
    clonedForm.get('restitutionInformation').markAsUntouched();

    this.state.data = null;
    return clonedForm;
  }

  buildApplicationForm(FORM: IOptionSetVal = this.FORM_TYPE): UntypedFormGroup {
    let group = {
      introduction: this.fb.group({}),
      restitutionInformation: this.restitutionInfoHelper.setupFormGroup(this.fb, FORM),
      totalAttachmentSize: [0]
    };

    return this.fb.group(group);
  }

  harvestForm(): iRestitutionApplication {
    let data = {
      ApplicationType: this.FORM_TYPE,
      RestitutionInformation: this.form.get('restitutionInformation').value
    } as iRestitutionApplication;

    return data;
  }

  submit() {
    this.form.markAllAsTouched();

    if (this.form.invalid) {
      // TODO: check show/hide validation message logic
      return;
    }

    const restitutionData = this.getRestitutionCreateRequest();
    this.restitutionsService.postApiRestitutions(restitutionData).subscribe({
      next: (res) => {
        console.log('success', res);
      },
      error: (err) => {
        console.log('error', err);
      }
    });
  }

  private getRestitutionCreateRequest(): CreateRestitutionCaseRequestDto {
    const formData = this.harvestForm();
    const restitutionInfo = formData.RestitutionInformation;

    const isVictimEntity = formData.ApplicationType.val === ResitutionForm.VictimEntity.val;
    const isVictimOrOffender =
      formData.ApplicationType.val === ResitutionForm.Victim.val ||
      formData.ApplicationType.val === ResitutionForm.Offender.val;

    const hasDesignate = restitutionInfo.authorizeDesignate && restitutionInfo.designate.length > 0;

    const primaryContact =
      restitutionInfo.contactInformation.entityContacts.find((contact) => contact?.isPrimaryContact === 100000000) ||
      restitutionInfo.contactInformation.entityContacts[0];

    const app: ApplicationDto = isVictimEntity
      ? {
          applicantType: ResitutionForm.Victim.val,
          applicantsFirstName: restitutionInfo.firstName,
          applicantsMiddleName: restitutionInfo.middleName,
          applicantsLastName: restitutionInfo.lastName,
          otherFirstName: restitutionInfo.otherFirstName,
          otherLastName: restitutionInfo.otherLastName,
          applicantsGenderCode: restitutionInfo.gender,
          genderIdentityText: restitutionInfo.otherGender,
          primaryRaceEthnicity: restitutionInfo.primaryRaceEthnicity,
          primaryRaceEthnicityText: restitutionInfo.otherPrimaryRaceEthnicity,
          pronouns: restitutionInfo.pronouns,
          pronounsText: restitutionInfo.otherPronoun,
          applicantsBirthDate: restitutionInfo.birthDate as any,
          indigenous: restitutionInfo.indigenousStatus,
          applicantsSignature: restitutionInfo.signature,
          smsPreferred: 100000000,
          applicantsPreferredMethodOfContact: primaryContact?.preferredMethodOfContact,
          applicantsPrimaryPhoneNumber: primaryContact?.phoneNumber,
          applicantsAlternatePhoneNumber: primaryContact?.alternatePhoneNumber,
          applicantsEmail: primaryContact?.email,
          applicantsPrimaryAddressLine1: restitutionInfo.contactInformation.mailingAddress.line1,
          applicantsPrimaryAddressLine2: restitutionInfo.contactInformation.mailingAddress.line2,
          applicantsPrimaryAddressLine3: '',
          applicantsPrimaryCity: restitutionInfo.contactInformation.mailingAddress.city,
          applicantsPrimaryProvince: restitutionInfo.contactInformation.mailingAddress.province,
          applicantsPrimaryPostalCode: restitutionInfo.contactInformation.mailingAddress.postalCode,
          applicantsPrimaryCountry: restitutionInfo.contactInformation.mailingAddress.country,
          voicemailOption: null,
          contactTitle: '',
          offenderCustodyLocation: ''
        }
      : {
          applicantType: formData.ApplicationType.val,
          applicantsFirstName: restitutionInfo.firstName,
          applicantsMiddleName: restitutionInfo.middleName,
          applicantsLastName: restitutionInfo.lastName,
          otherFirstName: restitutionInfo.otherFirstName,
          otherLastName: restitutionInfo.otherLastName,
          applicantsGenderCode: restitutionInfo.gender,
          applicantsBirthDate: restitutionInfo.birthDate as any,
          indigenous: restitutionInfo.indigenousStatus,
          applicantsPreferredMethodOfContact: null,
          smsPreferred: null,
          applicantsPrimaryPhoneNumber: '',
          applicantsAlternatePhoneNumber: '',
          applicantsEmail: '',
          applicantsPrimaryAddressLine1: '',
          applicantsPrimaryAddressLine2: '',
          applicantsPrimaryAddressLine3: restitutionInfo.contactInformation.attentionTo,
          applicantsPrimaryCity: '',
          applicantsPrimaryProvince: '',
          applicantsPrimaryPostalCode: '',
          applicantsPrimaryCountry: '',
          voicemailOption: null,
          applicantsSignature: restitutionInfo.signature,
          offenderCustodyLocation: '',
          primaryRaceEthnicity: restitutionInfo.primaryRaceEthnicity,
          pronouns: restitutionInfo.pronouns,
          pronounsText: restitutionInfo.otherPronoun,
          primaryRaceEthnicityText: restitutionInfo.otherPrimaryRaceEthnicity,
          genderIdentityText: restitutionInfo.otherGender
        };

    if (!isVictimEntity && !hasDesignate) {
      app.applicantsPreferredMethodOfContact = restitutionInfo.contactInformation.preferredMethodOfContact;
      app.smsPreferred = restitutionInfo.contactInformation.smsPreferred;
      app.applicantsPrimaryPhoneNumber = restitutionInfo.contactInformation.phoneNumber;
      app.applicantsAlternatePhoneNumber = restitutionInfo.contactInformation.alternatePhoneNumber;
      app.applicantsEmail = restitutionInfo.contactInformation.email;
      app.applicantsPrimaryAddressLine1 = restitutionInfo.contactInformation.mailingAddress.line1;
      app.applicantsPrimaryAddressLine2 = restitutionInfo.contactInformation.mailingAddress.line2;
      app.applicantsPrimaryCity = restitutionInfo.contactInformation.mailingAddress.city;
      app.applicantsPrimaryProvince = restitutionInfo.contactInformation.mailingAddress.province;
      app.applicantsPrimaryPostalCode = restitutionInfo.contactInformation.mailingAddress.postalCode;
      app.applicantsPrimaryCountry = restitutionInfo.contactInformation.mailingAddress.country;
      app.voicemailOption = restitutionInfo.contactInformation.leaveVoicemail;
    }

    if (restitutionInfo.signatureName) {
      app.declarationFullName = restitutionInfo.signatureName;
    }

    if (restitutionInfo.signerTitle) {
      app.signingOfficerTitle = restitutionInfo.signerTitle;
    }

    if (restitutionInfo.signatureDate) {
      app.declarationDate = restitutionInfo.signatureDate as any;
    }

    restitutionInfo.courtFiles.forEach((file) => {
      if (file && (file.firstName || file.middleName || file.lastName || file.relationship)) {
        app.offenderFirstName = file.firstName;
        app.offenderMiddleName = file.middleName;
        app.offenderLastName = file.lastName;
      }
    });

    const courtInfoCollection = restitutionInfo.courtFiles
      .filter((file) => file && (file.fileNumber || file.location))
      .map((file) => ({
        courtFileNumber: file.fileNumber,
        courtLocation: file.location
      }));

    const toParticipantMethodOfContact = (input: number): number | null => {
      if (input === 1) {
        return 100000000;
      }

      if (input === 4) {
        return 100000002;
      }

      if (input === 2) {
        return 100000001;
      }

      return null;
    };

    const providerCollection: ParticipantDto[] = [];

    if (hasDesignate) {
      const designate = restitutionInfo.designate[0];
      const designatePrimaryContact =
        restitutionInfo.contactInformation.entityContacts.find((contact) => contact?.isPrimaryContact === 100000000) ||
        restitutionInfo.contactInformation.entityContacts[0];

      const contactInfo = isVictimOrOffender ? restitutionInfo.contactInformation : null;

      restitutionInfo.contactInformation.entityContacts.forEach((contact) => {
        const address = contact?.mailingAddress ?? restitutionInfo.contactInformation.mailingAddress;
        const preferredMethodOfContact = isVictimOrOffender
          ? contactInfo?.preferredMethodOfContact
          : designatePrimaryContact?.preferredMethodOfContact;
        const smsPreferred = isVictimOrOffender ? contactInfo?.smsPreferred : designatePrimaryContact?.smsPreferred;

        let restContactPreferenceForUpdates = 0;
        if (preferredMethodOfContact === 1) {
          restContactPreferenceForUpdates = 100000000;
        } else if (preferredMethodOfContact === 4) {
          restContactPreferenceForUpdates = 100000001;
        } else if (preferredMethodOfContact === 2) {
          restContactPreferenceForUpdates = 100000002;
        }

        if (smsPreferred === CRMBoolean.True) {
          restContactPreferenceForUpdates = 100000003;
        }

        providerCollection.push({
          firstName: designate.firstName,
          lastName: designate.lastName,
          preferredName: designate.preferredName,
          relationship1: 'Designate',
          addressLine1: address?.line1,
          addressLine2: address?.line2,
          city: address?.city,
          province: address?.province,
          postalCode: address?.postalCode,
          country: address?.country,
          phoneNumber: contactInfo ? contactInfo.phoneNumber : contact?.phoneNumber,
          alternatePhoneNumber: contactInfo ? contactInfo.alternatePhoneNumber : contact?.alternatePhoneNumber,
          email: contactInfo ? contactInfo.email : contact?.email,
          voicemailOptions: contactInfo ? contactInfo.leaveVoicemail : contact?.leaveVoicemail,
          preferredMethodOfContact: toParticipantMethodOfContact(
            contactInfo ? contactInfo.preferredMethodOfContact : contact?.preferredMethodOfContact
          ),
          restContactPreferenceForUpdates,
          isPrimaryEntityContact: contact?.isPrimaryContact,
          title: contact?.contactTitle
        });
      });
    }

    if (
      formData.ApplicationType.val === ResitutionForm.Victim.val ||
      formData.ApplicationType.val === ResitutionForm.VictimEntity.val
    ) {
      restitutionInfo.courtFiles.forEach((file) => {
        providerCollection.push({
          firstName: restitutionInfo.firstName,
          middleName: restitutionInfo.middleName,
          lastName: restitutionInfo.lastName,
          relationship1: 'Victim',
          relationship2: 'Other',
          relationship2Other: file.relationship
        });
      });
    }

    if (
      (formData.ApplicationType.val === ResitutionForm.Victim.val ||
        formData.ApplicationType.val === ResitutionForm.VictimEntity.val) &&
      restitutionInfo.vsw &&
      restitutionInfo.vsw[0] &&
      Object.values(restitutionInfo.vsw[0]).some((value) => !!value)
    ) {
      const vsw = restitutionInfo.vsw[0];
      providerCollection.push({
        firstName: vsw.firstName,
        lastName: vsw.lastName,
        programName: vsw.program,
        phoneNumber: vsw.phoneNumber,
        email: vsw.email,
        relationship1: 'Victim Service Worker'
      });
    }

    if (formData.ApplicationType.val === ResitutionForm.VictimEntity.val) {
      restitutionInfo.contactInformation.entityContacts.forEach((contact) => {
        if (contact && Object.values(contact).some((value) => !!value)) {
          providerCollection.push({
            firstName: contact.firstName,
            lastName: contact.lastName,
            relationship1: 'Representative',
            preferredMethodOfContact: toParticipantMethodOfContact(contact.preferredMethodOfContact),
            phoneNumber: contact.phoneNumber,
            alternatePhoneNumber: contact.alternatePhoneNumber,
            voicemailOptions: contact.leaveVoicemail,
            email: contact.email,
            isPrimaryEntityContact: contact.isPrimaryContact,
            contactTitle: contact.contactTitle,
            smsPreferred: contact.smsPreferred
          });
        }
      });
    }

    const documentCollection = restitutionInfo.documents.map((document) => ({
      filename: document.filename,
      subject: document.subject,
      body: document.body
    }));

    return {
      application: app,
      courtInfoCollection: courtInfoCollection.length > 0 ? courtInfoCollection : null,
      providerCollection: providerCollection.length > 0 ? providerCollection : null,
      documentCollection: documentCollection.length > 0 ? documentCollection : null
    };
  }

  submitApplication() {
    this.markAsTouched();

    this.submitting = true;
    if (this.form.valid) {
      let formValue = this.harvestForm();
      let data = convertRestitutionToCRM(formValue);
      this.justiceDataService.submitRestitutionApplication(data).subscribe(
        (data) => {
          if (data['IsSuccess'] == true) {
            this.submitting = false;
            this.state.data = { type: this.FORM_TYPE, form: this.form };
            this.router.navigate(['/restitution-success']);
          } else {
            this.submitting = false;
            this.snackBar.open('Error submitting application. ' + data['message'], 'Close', {
              duration: 3500,
              panelClass: ['red-snackbar']
            });
            if (this.isIE) {
              alert('Encountered an error. Please use another browser as this may resolve the problem.');
            }
          }
        },
        (error) => {
          this.submitting = false;
          this.snackBar.open('Error submitting application', 'Close', { duration: 3500, panelClass: ['red-snackbar'] });
          if (this.isIE) {
            alert('Encountered an error. Please use another browser as this may resolve the problem.');
          }
        }
      );
    } else {
      this.submitting = false;
      this.markAsTouched();
    }
  }

  verifyCancellation(): void {
    let self = this;
    let dialogRef = this.matDialog.open(CancelDialog, {
      autoFocus: false,
      data: { type: 'Application' }
    });

    dialogRef.afterClosed().subscribe((res: any) => {
      if (res.cancel) {
        self.form = self.buildApplicationForm();

        // Clear any state data
        self.state.data = null;

        if (self.restitutionStepper) {
          self.restitutionStepper.reset();
        }

        self.router.navigate(['/']);
      }
    });
  }

  printApplication() {
    window.scroll(0, 0);
    this.showPrintView = true;
    document.querySelectorAll('.slide-close')[0].classList.add('hide-for-print');
    setTimeout(() => {
      window.print();
    }, 100);
  }

  @HostListener('window:afterprint')
  onafterprint() {
    document.querySelectorAll('.slide-close')[0].classList.remove('hide-for-print');
    window.scroll(0, document.body.scrollHeight);
    this.showPrintView = false;
  }
}
