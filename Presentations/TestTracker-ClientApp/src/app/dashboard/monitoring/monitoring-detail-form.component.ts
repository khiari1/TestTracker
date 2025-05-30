import { Component, Input, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { MonitoringService } from 'src/app/services/monitoring.service';

@Component({
  template: ` 
<div class="flex justify-content-center p-5" *ngIf="!data">
        <tsi-spinner label="Loading..." ></tsi-spinner>
      </div> 
  <div class="p-1" *ngIf="data">
    <div class="grid">
      <div class="col-12 flex justify-content-between">
        <div>
          <h3 class=" text-2xl">{{ data.nameMethodeTest }}</h3>
        </div>
        <div class="flex justify-center">
          <state-icon [value]="data.state"> </state-icon>
          <p-chip
            styleClass="ml-2"
            icon="pi pi-calendar"
            label="{{ data.date | date : 'dd/MM/yyyy' }}"
          ></p-chip>
          <p-chip
            styleClass="ml-2"
            icon="pi pi-clock"
            [label]="data.duration"
          ></p-chip>
        </div>
      </div>
      <div class="col-12">
        <p>{{ data.exceptionMessage }}</p>
      </div>
      <div class="col-12 flex">
        <div>
          <p class="font-bold">Area :</p>
        </div>
        <div>
          <p>
            {{ data.moduleName }} - {{ data.menuName }} -
          </p>
        </div>
      </div>
    </div>
    <div class="mt-4 grid bg-bluegray-900 border-round-sm p-4">
      <div class="col-12" [ngStyle]="{ color: 'var(--primary-color-text)' }">
        <div class="flex pb-2 align-items-center font-bold">
          <i class="pi pi-info-circle text-2xl mr-2"></i>
          <span class="vertical-align-middle text-2xl">Detail</span>
        </div>
        <div class="pb-1">
          <p
            class="text-2xl "
            [ngStyle]="{ color: 'var(--text-color-secondary)' }"
          >
            {{ data.exceptionType }}
          </p>
        <div [innerHTML]="data.errorMesage"></div>
      </div>
      <div class="col-12" *ngIf="data.stackTrace">
        <pre class="bg-bluegray-900 text-100">
        <code  [innerHTML]="data.stackTrace"></code>
      </pre>
      </div>
    </div>
  </div>`,
})
export class MonitoringDetailForm implements OnInit {
  @Input() data: any | undefined;

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    public monitoringService: MonitoringService
  ) {}
  ngOnInit(): void {

    this.monitoringService.getDetailsById(this.config.data.toString()).subscribe(data=> {
        this.data = data[0]
    });
  }
}
