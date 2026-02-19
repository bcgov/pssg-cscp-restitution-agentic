import { Component, Input, OnInit } from '@angular/core';
import { ControlContainer, UntypedFormArray, UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatLegacyDialog as MatDialog, MatLegacyDialogConfig as MatDialogConfig } from '@angular/material/legacy-dialog';
import { iLookupData } from '../../../interfaces/lookup-data.interface';
import { LookupService } from '../../../services/lookup.service';
import { SignPadDialog } from '../../../sign-dialog/sign-dialog.component';
import { AddressHelper } from '../../address/address.helper';
import { CRMMultiBoolean, IOptionSetVal, MY_FORMATS, ResitutionForm } from '../../enums-list';
import { FormBase } from '../../form-base';
import { POSTAL_CODE } from '../../regex.constants';
import { RestitutionInfoHelper } from './restitution-information.helper';

@Component({
  selector: 'app-restitution-information',
  templateUrl: './restitution-information.component.html',
  styleUrls: ['./restitution-information.component.scss'],
  providers: [
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS }
  ]
})
export class RestitutionInformationComponent extends FormBase implements OnInit {
  @Input() formType: IOptionSetVal;
  @Input() lookupData: iLookupData;
  @Input() isDisabled: boolean;
  public form: UntypedFormGroup;
  ResitutionForm = ResitutionForm;
  postalRegex = POSTAL_CODE;
  CRMMultiBoolean = CRMMultiBoolean;

  page_header: string = '';
  applicant_type: string = '';

  addressHelper = new AddressHelper();
  phoneMinLength: number = 10;
  phoneMaxLength: number = 15;

  relationshipList: any = [];
  courtList: any = [];

  restitutionInfoHelper = new RestitutionInfoHelper();

  constructor(
    private controlContainer: ControlContainer,
    private fb: UntypedFormBuilder,
    private matDialog: MatDialog,
    public lookupService: LookupService
  ) {
    super();
  }

  ngOnInit() {
    this.form = <UntypedFormGroup>this.controlContainer.control;
    setTimeout(() => {
      this.form.markAsTouched();
    }, 0);
    // console.log("restitution info component");
    // console.log(this.form);

    if (this.formType.val === ResitutionForm.Victim.val) {
      this.page_header = 'Victim Application';
      this.applicant_type = 'Victim';
    } else if (this.formType.val === ResitutionForm.Offender.val) {
      this.page_header = 'Accused/Offender Application';
      this.applicant_type = 'Applicant';
    } else if (this.formType.val === ResitutionForm.VictimEntity.val) {
      this.page_header = 'Entity Victim Application';
      this.applicant_type = 'Victim';
      this.clearControlValidators(this.form.get('firstName'));
      this.clearControlValidators(this.form.get('birthDate'));
      this.clearControlValidators(this.form.get('indigenousStatus'));
      this.clearControlValidators(this.form.get('authorizeDesignate'));
    }

    if (this.lookupData.courts && this.lookupData.courts.length > 0) {
      this.courtList = this.lookupData.courts.map((c) => c.vsd_name);
    } else {
      this.lookupService.getCourts().subscribe((res) => {
        this.lookupData.courts = res.value;
        if (this.lookupData.courts) {
          this.lookupData.courts.sort(function (a, b) {
            return a.vsd_name.localeCompare(b.vsd_name);
          });
        }
        this.courtList = this.lookupData.courts.map((c) => c.vsd_name);
      });
    }

    if (this.lookupData.relationships && this.lookupData.relationships.length > 0) {
      this.relationshipList = this.lookupData.relationships.map((r) => r.vsd_name);
    } else {
      this.lookupService.getRestitutionRelationships().subscribe((res) => {
        this.lookupData.relationships = res.value;
        if (this.lookupData.relationships) {
          this.lookupData.relationships.sort(function (a, b) {
            return a.vsd_name.localeCompare(b.vsd_name);
          });
        }
        this.relationshipList = this.lookupData.relationships.map((r) => r.vsd_name);
      });
    }
  }

  iHaveOtherNamesChange(val: boolean) {
    if (!val) {
      let otherFirstNameControl = this.form.get('otherFirstName');
      let otherLastNameControl = this.form.get('otherLastName');

      otherFirstNameControl.patchValue('');
      otherLastNameControl.patchValue('');
    }
  }

  authorizeDesignateChange() {
    if (this.form.get('authorizeDesignate').value === CRMMultiBoolean.True) {
      this.addDesignate();
    } else {
      this.removeDesignate();
    }
  }

  addDesignate() {
    let designate = this.form.get('designate') as UntypedFormArray;
    if (designate.length == 0) {
      designate.push(this.restitutionInfoHelper.createDesignate(this.fb));
    }
  }

  removeDesignate() {
    let designate = this.form.get('designate') as UntypedFormArray;
    while (designate.length > 0) {
      designate.removeAt(0);
    }
  }

  addCourtFile() {
    let courtFiles = this.form.get('courtFiles') as UntypedFormArray;
    courtFiles.push(this.restitutionInfoHelper.createCourtFile(this.fb, this.formType));
  }

  removeCourtFile(index: number) {
    let courtFiles = this.form.get('courtFiles') as UntypedFormArray;
    courtFiles.removeAt(index);
  }

  showSignPad(control): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.matDialog.open(SignPadDialog, dialogConfig);
    dialogRef.afterClosed().subscribe(
      (data) => {
        var patchObject = {};
        patchObject[control] = data;
        this.form.patchValue(patchObject);
      },
      (err) => console.log(err)
    );
  }
}
