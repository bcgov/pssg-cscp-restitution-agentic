import { CdkTableModule } from '@angular/cdk/table';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatNativeDateModule, MatRippleModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyAutocompleteModule as MatAutocompleteModule } from '@angular/material/legacy-autocomplete';
import { MatLegacyButtonModule as MatButtonModule } from '@angular/material/legacy-button';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatLegacyListModule as MatListModule } from '@angular/material/legacy-list';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyProgressBarModule as MatProgressBarModule } from '@angular/material/legacy-progress-bar';
import { MatLegacyProgressSpinnerModule as MatProgressSpinnerModule } from '@angular/material/legacy-progress-spinner';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacySlideToggleModule as MatSlideToggleModule } from '@angular/material/legacy-slide-toggle';
import { MatLegacySliderModule as MatSliderModule } from '@angular/material/legacy-slider';
import { MatLegacySnackBarModule as MatSnackBarModule } from '@angular/material/legacy-snack-bar';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSortModule } from '@angular/material/sort';
import { MatStepperModule } from '@angular/material/stepper';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// import { AngularSignaturePadModule } from '@almothafar/angular-signature-pad'; // TODO: Re-enable when upgrading to Angular 10+
import { AlertModule } from 'ngx-bootstrap/alert';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { NgxFileDropModule } from 'ngx-file-drop';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { FeatureEnabledDirective } from './directives/feature-enabled.directive';
import { NotFoundComponent } from './not-found/not-found.component';
import { PhonePipe } from './pipes/phone.pipe';
import { QuickExitComponent } from './quick-exit/quick-exit.component';
import { RestitutionApplicationComponent } from './restitution-application/restitution-application.component';
import { JusticeApplicationDataService } from './services/justice-application-data.service';
import { LookupService } from './services/lookup.service';
import { StateService } from './services/state.service';
import { CancelApplicationDialog } from './shared/cancel-dialog/cancel-dialog.component';
import { DateFieldComponent } from './shared/date-field/date-field.component';
import { CancelDialog } from './shared/dialogs/cancel/cancel.dialog';
import { MessageDialog } from './shared/dialogs/message-dialog/message.dialog';
import { FieldComponent } from './shared/field/field.component';
import { FileUploaderComponent } from './shared/file-uploader/file-uploader.component';
import { GenderSelectorComponent } from './shared/gender-selector/gender-selector.component';
import { RaceSelectorComponent } from './shared/race-selector/race-selector.component';
import { RestitutionAddressComponent } from './shared/restitution-address/address.component';
import { RestitutionContactInformationComponent } from './shared/restitution/contact-information/contact-information.component';
import { RestitutionInformationComponent } from './shared/restitution/restitution-information/restitution-information.component';
import { RestitutionOverviewComponent } from './shared/restitution/restitution-overview/restitution-overview.component';
import { RestitutionReviewComponent } from './shared/restitution/review/restitution-review.component';
import { RestitutionSuccessComponent } from './shared/restitution/success/restitution-success.component';
import { ServiceNotAvailableComponent } from './shared/service-not-available.component';
import { ToolTipTriggerComponent } from './shared/tool-tip/tool-tip.component';
import { SignPadDialog } from './sign-dialog/sign-dialog.component';

@NgModule({
  declarations: [
    RestitutionAddressComponent,
    AppComponent,
    BreadcrumbComponent,
    CancelApplicationDialog,
    CancelDialog,
    DateFieldComponent,
    FieldComponent,
    FileUploaderComponent,
    MessageDialog,
    NotFoundComponent,
    PhonePipe,
    QuickExitComponent,
    RestitutionApplicationComponent,
    RestitutionContactInformationComponent,
    RestitutionInformationComponent,
    RestitutionOverviewComponent,
    RestitutionReviewComponent,
    RestitutionSuccessComponent,
    SignPadDialog,
    ToolTipTriggerComponent,
    GenderSelectorComponent,
    RaceSelectorComponent,
    FeatureEnabledDirective,
    ServiceNotAvailableComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    CdkTableModule,
    NgxFileDropModule,
    NgxMaskDirective,
    NgxMaskPipe,
    FormsModule,
    HttpClientModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSlideToggleModule,
    MatSliderModule,
    MatSnackBarModule,
    MatSortModule,
    MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    ReactiveFormsModule,
    // AngularSignaturePadModule, // TODO: Re-enable when upgrading to Angular 10+
    AlertModule.forRoot(),
    BsDatepickerModule.forRoot(),
    TooltipModule.forRoot(),
    TypeaheadModule.forRoot()
  ],
  exports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    CdkTableModule,
    NgxFileDropModule,
    FormsModule,
    HttpClientModule,
    // HttpModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSlideToggleModule,
    MatSliderModule,
    MatSnackBarModule,
    MatSortModule,
    MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    ReactiveFormsModule,
    TooltipModule
  ],
  providers: [provideNgxMask(), JusticeApplicationDataService, LookupService, StateService, Title],
  bootstrap: [AppComponent]
})
export class AppModule {}
