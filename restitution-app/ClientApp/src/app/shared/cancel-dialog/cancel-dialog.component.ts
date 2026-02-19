import { Component, Inject, OnInit } from '@angular/core';
import { MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA, MatLegacyDialogRef as MatDialogRef } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-cancel-dialog',
  templateUrl: './cancel-dialog.component.html'
})
export class CancelApplicationDialog implements OnInit {
  applicationType: string;

  constructor(
    public dialogRef: MatDialogRef<CancelApplicationDialog>,
    private router: Router,
    private route: ActivatedRoute,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  closeMe(): void {
    this.dialogRef.close(false);
  }

  cancelApplication(): void {
    this.dialogRef.close(true);
  }

  ngOnInit() {
    this.applicationType = this.data;
  }
}
