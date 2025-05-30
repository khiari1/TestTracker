import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { MonitoringService } from 'src/app/services/monitoring.service';

@Component({
  template: `
    <p-messages></p-messages>
    <p-chip styleClass="mb-2" [label]="'Selected test ' + jobModel.monitoringIds.length"></p-chip>
    <div class="card">
      <div class="p-fluid">
        <div class="field">
          <label for="jobId">{{ 'Name' | translate }}</label>
          <input
            pInputText
            type="text"
            id="jobId"
            [(ngModel)]="jobModel.jobId"
          />
        </div>
        <div class="field">
          <label for="moduleId">Job mode</label>
          <p-dropdown
            [disabled]="readOnly"
            [options]="[
              { key: 0, value: 'Schedule' },
              { key: 1, value: 'Recurring' }
            ]"
            optionLabel="value"
            optionValue="key"
            [(ngModel)]="this.jobModel.jobMode"
          ></p-dropdown>
        </div>
        <div *ngIf="jobModel.jobMode == 0" class="field">
        <label for="minute">Date</label>
            <p-calendar [showTime]="true"  [(ngModel)]="jobModel.date"></p-calendar>
        </div>
        <div *ngIf="jobModel.jobMode == 1">
        <div class="field">
          <label for="moduleId">Reccuring Mode</label>
          <p-dropdown
            [disabled]="readOnly"
            [options]="[
              { key: 0, value: 'Hourly' },
              { key: 1, value: 'Daily' },
              { key: 2, value: 'Weekly' },
              { key: 3, value: 'Monthly' }
            ]"
            optionLabel="value"
            optionValue="key"
            [(ngModel)]="this.jobModel.recuringMode"
          ></p-dropdown>
        </div>
        <div class="field">
          <div class="grid">
            <div class="col-3  field">
              <label for="minute">Minute</label>
              <p-inputNumber inputId="minute" [(ngModel)]="jobModel.minute">
              </p-inputNumber>
            </div>
            <div class="col-3 field">
              <label for="hour">Hour</label>
              <p-inputNumber inputId="hour" [(ngModel)]="jobModel.hour">
              </p-inputNumber>
            </div>
            <div class="col-3 field">
              <label for="minuteInterval">Day Of Week</label>
              <p-inputNumber
                inputId="minuteInterval"
                [(ngModel)]="jobModel.dayOfWeek"
              >
              </p-inputNumber>
            </div>
            <div class="col-3 field">
              <label for="month">Month</label>
              <p-inputNumber inputId="month" [(ngModel)]="jobModel.month">
              </p-inputNumber>
            </div>
          </div>
        </div>
        </div>

      </div>
    </div>
    <div class="flex justify-content-end mt-3">
        <tsi-menu-bar [menuItems]="menuItems"></tsi-menu-bar>
    </div>
  `,
})
export class CreateJobComponent implements OnInit {
  /**
   *
   */
  jobModel: IJobModel = {
    hour: 1,
    minute: 1,
    dayOfWeek: 1,
    month: 1,
    jobId: '',
    jobMode: 0,
    recuringMode: 0,
    monitoringIds : [],
    date : new Date()
  };

  menuItems: MenuItem[] = [
    {
      label: 'Save',
      icon: 'pi pi-check',
      styleClass: 'bg-green-600 text-white',
      command: () => this.recurringJob(),
    },
    {
      label: 'Discard',
      icon: 'pi pi-times',
      command: () => this.dialogRef?.close(),
    },
  ];
  
  readOnly: boolean = false;
  constructor(
    public monitoringService: MonitoringService,
    public dialogRef: DynamicDialogRef,
    public dialogOption: DynamicDialogConfig
  ) {}
  ngOnInit(): void {
    if (this.dialogOption.data) {
      this.jobModel = this.dialogOption.data;
      this.readOnly = true;
    }
  }

  recurringJob() {
    if(this.jobModel.jobMode == 0){
      this.monitoringService.createScheduleJob(this.jobModel);
    }else{
      this.monitoringService.createRecurringJob(this.jobModel);
    }
    
    this.dialogRef.close();
  }
}
export interface IJobModel {
  hour: number;
  minute: number;
  dayOfWeek: number;
  month: number;
  jobId: string;
  jobMode: number;
  recuringMode: number;
  monitoringIds : number[];
  date : Date;
}
