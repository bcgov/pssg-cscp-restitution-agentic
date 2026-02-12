import { CdkTableModule } from '@angular/cdk/table';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
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
  MatSliderModule,
  MatSlideToggleModule,
  MatSnackBarModule,
  MatSortModule,
  MatStepperModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatTooltipModule
} from '@angular/material';
import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Angular2SignaturepadModule } from 'angular2-signaturepad';
import { NgBusyModule } from 'ng-busy';
import { AlertModule, BsDatepickerModule } from 'ngx-bootstrap';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { CookieService } from 'ngx-cookie-service';
import { FileDropModule } from 'ngx-file-drop';
import { NgxMaskModule } from 'ngx-mask';
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
    FileDropModule,
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
    NgBusyModule,
    ReactiveFormsModule,
    Angular2SignaturepadModule,
    AlertModule.forRoot(),
    BsDatepickerModule.forRoot(),
    NgxMaskModule.forRoot(),
    TooltipModule.forRoot(),
    TypeaheadModule.forRoot()
  ],
  exports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    CdkTableModule,
    FileDropModule,
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
  providers: [CookieService, JusticeApplicationDataService, LookupService, StateService, Title],
  entryComponents: [CancelApplicationDialog, CancelDialog, MessageDialog, SignPadDialog, ServiceNotAvailableComponent],
  bootstrap: [AppComponent]
})
export class AppModule {}
