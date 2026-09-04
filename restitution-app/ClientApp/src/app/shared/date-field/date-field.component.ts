import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-date-field',
  templateUrl: './date-field.component.html',
  styleUrls: ['./date-field.component.scss'],
  standalone: false
})
export class DateFieldComponent implements OnInit {
  @Input() control: AbstractControl;
  @Input() max: Date;
  @Input() min: Date;
  @Input() disabled: boolean;
  dayList = [];
  yearList = [];

  day = 0;
  month = -1;
  year = 0;

  currentYear = new Date().getFullYear();

  constructor() {}

  ngOnInit() {
    const value = this.control.value;
    const date = value instanceof Date ? value : value ? new Date(value) : null;
    if (date && !isNaN(date.getTime())) {
      this.year = date.getFullYear();
      this.month = date.getMonth();
      this.day = date.getDate();
    }

    for (let i = 1; i <= 31; ++i) {
      this.dayList.push(i);
    }

    for (let i = 0; i < 120; ++i) {
      this.yearList.push(this.currentYear - i);
    }
  }

  output() {
    if (this.day == 0 || this.month == -1 || this.year == 0) {
      this.control.patchValue(null);
      return;
    }

    this.control.markAsTouched();

    let hasMinError = false;
    let hasMaxError = false;

    const date = new Date(this.year, this.month, this.day);
    if (this.min) {
      if (date < this.min) {
        hasMinError = true;
        setTimeout(() => {
          this.control.setErrors({ incorrect: true });
        }, 0);
      }
    }

    if (this.max) {
      if (date > this.max) {
        hasMaxError = true;
        setTimeout(() => {
          this.control.setErrors({ incorrect: true });
        }, 0);
      }
    }

    if (!hasMinError && !hasMaxError) {
      setTimeout(() => {
        this.control.setErrors(null);
      }, 0);
    }

    this.control.patchValue(date);
  }
}
