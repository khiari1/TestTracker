import { Component, Input, OnInit } from '@angular/core';
import { Title } from 'chart.js/dist/plugins/plugin.title';

@Component({
  selector: 'state-icon',
  template: `
  <div class="flex align-items-center">
      <img
        class="mr-1"
        [style]="'width: 20px; height: 20px;'"
        [src]="'assets/svg/'+severity"
        [title]="title"
      />
      <span class="font-normal">{{title}}</span>
    </div>
<!-- 
    <img
      style="width: 23px; height: 23px;"
      *ngIf="value === 0"
      src="assets/svg/success.svg"
      title="Success"
    />
    <img
      style="width: 23px; height: 23px;"
      *ngIf="value === 3"
      src="assets/svg/error.svg"
      title="Error"
    />
    <img
      style="width: 23px; height: 23px;"
      *ngIf="value === 2"
      src="assets/svg/error.svg"
      title="Failed"
    />
    <img
      style="width: 23px; height: 23px;"
      *ngIf="value === 1"
      src="assets/svg/warning-24.png"
      title="warning"
    /> -->
    `,
})
export class StateIconComponent implements OnInit {
  @Input() value: number = 0;

  get severity(): string {
    switch (this.value) {
      case 0:
        return 'success.svg';
      case 1:
        return 'warning.png';
      case 2:
        return 'error.svg';
      case 3:
        return 'no-entry.png';
      case 4:
        return 'skipederror.svg';
        default : 
      return ''
    }
  }

    get title(): string {
      switch (this.value) {
        case 0:
          return 'Success';
        case 1:
          return 'Warning';
        case 2:
          return 'Failed';
        case 3:
          return 'Error';
        case 4:
          return 'Skipped';
          default : 
        return ''
      }
  }

  constructor() {}

  ngOnInit(): void {}
}
