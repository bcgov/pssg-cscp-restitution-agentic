import { Component, Inject } from '@angular/core';
import { MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA, MatLegacyDialogRef as MatDialogRef } from '@angular/material/legacy-dialog';

@Component({
  selector: 'app-cancel',
  templateUrl: 'cancel.dialog.html',
  styleUrls: ['./cancel.dialog.scss']
})
export class CancelDialog {
  type: string = 'Application';
  constructor(public dialogRef: MatDialogRef<CancelDialog>, @Inject(MAT_DIALOG_DATA) public data: any) {
    this.type = data.type || 'Application';
  }

  cancel() {
    this.dialogRef.close({ cancel: true });
  }

  closeMe() {
    this.dialogRef.close({ cancel: false });
  }
}
