import { Component } from '@angular/core';
import { ControlContainer, UntypedFormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { LookupsService as ApiLookupsService } from '../../../../api/lookups/lookups.service';
import { ResitutionForm } from '../../enums-list';
import {
  RESTITUTION_INFORMATION_PROVIDERS,
  RestitutionInformationComponent
} from './restitution-information.component';

@Component({
  selector: 'app-victim-entity-form',
  templateUrl: './victim-entity-form.component.html',
  styleUrls: ['./restitution-information.component.scss'],
  providers: RESTITUTION_INFORMATION_PROVIDERS,
  standalone: false
})
export class VictimEntityFormComponent extends RestitutionInformationComponent {
  override formType = ResitutionForm.VictimEntity;

  constructor(
    controlContainer: ControlContainer,
    fb: UntypedFormBuilder,
    matDialog: MatDialog,
    apiLookupsService: ApiLookupsService
  ) {
    super(controlContainer, fb, matDialog, apiLookupsService);
  }

  // TODO: create form class, move form logic
}
