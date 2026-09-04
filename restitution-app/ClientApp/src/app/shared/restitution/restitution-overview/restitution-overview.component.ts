import { Component, Input, OnInit } from '@angular/core';
import { ControlContainer, UntypedFormGroup } from '@angular/forms';
import { DateAdapter, MAT_DATE_FORMATS, MAT_NATIVE_DATE_FORMATS, NativeDateAdapter } from '@angular/material/core';
import { IOptionSetVal, ResitutionForm } from '../../enums-list';
import { FormBase } from '../../form-base';

@Component({
  selector: 'app-restitution-overview',
  templateUrl: './restitution-overview.component.html',
  styleUrls: ['./restitution-overview.component.scss'],
  providers: [
    { provide: DateAdapter, useClass: NativeDateAdapter },
    { provide: MAT_DATE_FORMATS, useValue: MAT_NATIVE_DATE_FORMATS }
  ],
  standalone: false
})
export class RestitutionOverviewComponent extends FormBase implements OnInit {
  @Input() formType: IOptionSetVal;
  public form: UntypedFormGroup;
  ResitutionForm = ResitutionForm;
  applicant: string = '';

  constructor(private controlContainer: ControlContainer) {
    super();
  }

  ngOnInit() {
    this.form = <UntypedFormGroup>this.controlContainer.control;
    setTimeout(() => {
      this.form.markAsTouched();
    }, 0);

    if (this.formType.val === ResitutionForm.Victim.val || this.formType.val === ResitutionForm.VictimEntity.val) {
      this.applicant = 'Victim';
    } else if (this.formType.val === ResitutionForm.Offender.val) {
      this.applicant = 'Accused/Offender';
    }
  }
}
