import { Component, OnInit } from '@angular/core';
import { Message, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { firstValueFrom } from 'rxjs';
import { IKeyValuePair } from 'src/app/models/Ikey-value-pair.interface';
import { MonitoringService } from 'src/app/services/monitoring.service';
import { ResourceService } from 'src/app/services/resource.service';
import { MonitoringDetailForm } from './monitoring-detail-form.component';
interface BreadcrumbLink {
  routerLink: string;
  queryParams?: { [key: string]: any };
  label: string;
}
@Component({
  selector: 'app-monitoring',
  template: `
    <form-view
      [dataSource]="'Monitorings'"
      [(data)]="monitoring"
      [breadcrumbLinks]="breadcrumbLinks"
      iconSrc="assets/png/icons8-report-file-48.png"
    >
    <div *ngIf="monitoring">
    <ng-template #titleTemplate>
        <div class="flex flex-column">
          <p class="font-normal text-2xl pb-2">
            Monitoring | {{ monitoring.nameMethodTest }} 
          </p>
          <div
            class="flex justify-center"
            *ngIf="
              monitoring.monitoringDetails &&
              monitoring.monitoringDetails.length > 0
            "
          >
            <state-icon [value]="monitoring.monitoringDetails[0].status">
            </state-icon>
            <p-chip
              styleClass="ml-2"
              icon="pi pi-calendar"
              label="{{
                this.monitoring.monitoringDetails[0].date | date : 'dd/MM/yyyy'
              }}"
            ></p-chip>
            <p-chip
              styleClass="ml-2"
              icon="pi pi-clock"
              [label]="this.monitoring.monitoringDetails[0].duration"
            ></p-chip>
          </div>
        </div>
      </ng-template>

      <ng-template #content>
      <p-messages
        *ngIf="
          monitoring?.monitoringDetails &&
          monitoring.monitoringDetails.length > 0
          
        "
        [value]="[
          {
            severity: monitoring.monitoringDetails[0].status === 0 ?'success':'error',
            summary: 'error',
            detail: monitoring.monitoringDetails[0].message ?? ''
          }
        ]"
        [enableService]="false"
        [closable]="false"
      ></p-messages>
          <p-tabView>
            <p-tabPanel header="Detail">
              <ng-template pTemplate="header">
                <div class="flex align-items-center">
                  <i class="pi pi-info-circle mr-2"></i>
                  <span class="vertical-align-middle">General Info</span>
                </div>
              </ng-template>
              <div class="grid" *ngIf="monitoring">
                <div class="col-5">
                  <div class="p-fluid">
                    <div class="field grid">
                      <label
                        htmlfor="name3"
                        class="col-12 mb-2 md:col-2 md:mb-0"
                        >{{ 'Titre' | translate }}</label
                      >
                      <div class="col-12 md:col-10">
                        <input
                          type="text"
                          [(ngModel)]="monitoring.nameMethodTest"
                          pInputText
                        />
                      </div>
                    </div>
                    <div class="field grid">
                      <label
                        htmlfor="name3"
                        class="col-12 mb-2 md:col-2 md:mb-0"
                        >{{ 'TestMethode' | translate }}</label
                      >
                      <div class="col-12 md:col-10">
                        <input
                          type="text"
                          [(ngModel)]="monitoring.nameMethodTest"
                          pInputText
                        />
                      </div>
                    </div>
                    <div class="field grid">
                      <label
                        htmlfor="name3"
                        class="col-12 mb-2 md:col-2 md:mb-0"
                        >{{ 'Module' | translate }}</label
                      >
                      <div class="col-12 md:col-10">
                        <p-dropdown
                          [options]="modules"
                          [(ngModel)]="selectedModule"
                          optionLabel="value"
                          optionValue="key"
                          placeholder = 'Select module'
                        ></p-dropdown>
                      </div>
                    </div>
                    <div class="field grid">
                      <label
                        htmlfor="name3"
                        class="col-12 mb-2 md:col-2 md:mb-0"                    
                        >{{ 'Responsible' | translate }}</label
                      >
                      <div class="col-12 md:col-10">
                        <p-dropdown
                          [options]="users"
                          [(ngModel)]="monitoring.responsibleId"
                          (onChange)="selectResponsible($event)"
                          optionLabel="value"
                          optionValue="key"
                        >
                          <ng-template pTemplate="selectedItem">
                            <div
                              class="flex align-items-center gap-2"
                              *ngIf="selectedResponsible"
                            >
                              <tsi-avatar
                                [size]="'small'"
                                [label]="selectedResponsible"
                                [src]="
                                  'https://localhost:7092/Avatar/' +
                                  monitoring.responsibleId +
                                  '.png'
                                "
                              ></tsi-avatar>
                            </div>
                          </ng-template>
                          <ng-template let-user pTemplate="item">
                            <tsi-avatar
                              [size]="'small'"
                              [label]="user.value"
                              [src]="
                                'https://localhost:7092/Avatar/' +
                                user.key +
                                '.png'
                              "
                            ></tsi-avatar>
                          </ng-template>
                        </p-dropdown>
                      </div>
                    </div>
                    <div class="field grid">
                      <label
                        htmlfor="name3"
                        class="col-12 mb-2 md:col-2 md:mb-0"
                        >{{ 'Tester' | translate }}</label
                      >
                      <div class="col-12 md:col-10">
                        <p-dropdown
                          [options]="users"
                          [(ngModel)]="monitoring.testerId"
                          (onChange)="selectTester($event)"
                          optionLabel="value"
                          optionValue="key"
                        >
                          <ng-template pTemplate="selectedItem">
                            <div
                              class="flex align-items-center gap-2"
                              *ngIf="selectedTester"
                            >
                              <tsi-avatar
                                [size]="'small'"
                                [label]="selectedTester"
                                [src]="
                                  'https://localhost:7092/Avatar/' +
                                  monitoring.testerId +
                                  '.png'
                                "
                              ></tsi-avatar>
                            </div>
                          </ng-template>
                          <ng-template let-user pTemplate="item">
                            <tsi-avatar
                              [size]="'small'"
                              [label]="user.value"
                              [src]="
                                'https://localhost:7092/Avatar/' +
                                user.key +
                                '.png'
                              "
                            ></tsi-avatar>
                          </ng-template>
                        </p-dropdown>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-7">
                  <p-accordion [multiple]="true">
                    <p-accordionTab>
                      <ng-template pTemplate="header">
                        <div class="flex align-items-center">
                          <i class="pi pi-paperclip mr-2"></i>
                          <span class="vertical-align-middle">Use Case</span>
                        </div>
                      </ng-template>
                      <div>
                        <p-editor
                          [(ngModel)]="monitoring.useCase"
                          [style]="{ height: '250px' }"
                        ></p-editor>
                      </div>
                    </p-accordionTab>
                    <p-accordionTab>
                      <ng-template pTemplate="header">
                        <div class="flex align-items-center">
                          <i class="pi pi-paperclip mr-2"></i>
                          <span class="vertical-align-middle"
                            >Awaited result</span
                          >
                        </div>
                      </ng-template>

                      <p-editor
                        [(ngModel)]="monitoring.awaitedResult"
                        [style]="{ height: '250px' }"
                      ></p-editor>
                    </p-accordionTab>
                  </p-accordion>
                  <p-accordion [multiple]="true">
                    <p-accordionTab>
                      <ng-template pTemplate="header">
                        <div class="flex align-items-center">
                          <i class="pi pi-comment mr-2"></i>
                          <span class="vertical-align-middle">Comment</span>
                        </div>
                      </ng-template>

                      <div *ngIf="monitoring">
                        <app-comment
                          [relatedObject]="monitoring.id"
                          keyGroup="Monitoring"
                        ></app-comment>
                      </div>
                    </p-accordionTab>
                  </p-accordion>
                </div>
              </div>
            </p-tabPanel>
            <p-tabPanel header="History">
              <ng-template pTemplate="header">
                <div class="flex align-items-center">
                  <i class="pi pi-clock mr-2"></i>
                  <span class="vertical-align-middle">Histories</span>
                </div>
              </ng-template>
              <p-table
                [value]="monitoring.monitoringDetails"
                dataKey="id"
                styleClass="p-datatable-sm"
                selectionMode="single"
              >
                <ng-template pTemplate="header">
                  <tr>
                    <th width="120px" pSortableColumn="state">
                    {{ 'Status' | translate }}
                      <p-sortIcon field="testResult"></p-sortIcon>
                    </th>
                    <th width="200px" pSortableColumn="exceptionType">
                      {{ 'ExceptionType' | translate }}
                      <p-sortIcon field="exceptionType"></p-sortIcon>
                    </th>
                    <th pSortableColumn="message">
                      {{ 'Message' | translate }}
                      <p-sortIcon field="message"></p-sortIcon>
                    </th>
                    <th width="130px" pSortableColumn="date">
                      {{ 'Date' | translate }}
                      <p-sortIcon field="date"></p-sortIcon>
                    </th>
                    <th width="130px" pSortableColumn="duration">
                      {{ 'Duration' | translate }}
                      <p-sortIcon field="duration"></p-sortIcon>
                    </th>
                  </tr>
                </ng-template>
                <ng-template pTemplate="body" let-monitoringDetail>
                  <tr>
                    <td>
                      <state-icon [value]="monitoringDetail.status">
                      </state-icon>
                    </td>
                    <td>
                      {{ monitoringDetail.exceptionType }}
                    </td>
                    <td>
                      {{ monitoringDetail.message }}
                      <span class="ml-2 font-normal text-blue-300 line-height-3 cursor-pointer hover:underline" (click)="openModal(monitoringDetail.id)">
                            {{'More detail' | translate}}
                    </span>
                    </td>
                    <td>
                      <i class="pi pi-calendar"></i>
                      {{ monitoringDetail.date | date : 'dd/MM/yyyy' }}
                    </td>
                    <td>
                      <i class="pi pi-clock"></i>
                      {{ monitoringDetail.duration }}
                    </td>
                  </tr>
                </ng-template>
              </p-table>
            </p-tabPanel>
            <p-tabPanel>
              <ng-template pTemplate="header">
                <div class="flex align-items-center">
                  <i class="pi pi-paperclip mr-2"></i>
                  <span class="vertical-align-middle">Attachement</span>
                </div>
              </ng-template>
              <div *ngIf="monitoring?.id">
                <app-attachement
                  [objectId]="monitoring.id"
                  keyGroup="Monitoring"
                ></app-attachement>
              </div>
            </p-tabPanel>
          </p-tabView>
      </ng-template>
    </div>
      
    </form-view>
  `,
  providers: [MessageService,DialogService],
})
export class MonitoringFormComponent implements OnInit {

  selectResponsible(selected: any) {
    console.log(selected);
    this.selectedResponsible =
      this.users.find((u) => u.key === selected.value)?.value ?? '';
  }

  selectTester(selected: any) {
    this.selectedTester =
      this.users.find((u) => u.key === selected.value)?.value ?? '';
  }

  monitoring!: any;

  message!: Message[];

  users: IKeyValuePair<string, string>[] = [];

  modules: IKeyValuePair<number, string>[] = [];
  selectedResponsible: string = '';

  selectedTester: string = '';

  breadcrumbLinks: BreadcrumbLink[] = [
    {
      routerLink: '/dashboard',
      label: 'Dashboard',
    },
    {
      routerLink: '/dashboard/monitoring',
      label: 'monitoring',
    },
    {
      routerLink: '/dashboard/monitoring/editmonitoring',
      label: 'Edition Monitoring',
    },
  ];
  
  private _selectedModule: number | undefined;

  set selectedModule(value: number | undefined) {
    this._selectedModule = value;
    if (this._selectedModule === undefined || this._selectedModule === null)
      return;
  }
  get selectedModule(): number | undefined {
    return this._selectedModule;
  }

 

 

  constructor(
    private monitoringService: MonitoringService,
    private resourceService: ResourceService,
    private dialogService : DialogService
  ) {}

  async ngOnInit(): Promise<void> {
    
    this.modules = await firstValueFrom(this.resourceService.getModules());

    this.users = await firstValueFrom(this.resourceService.getAllUsers());

    this.initData();


  }

  initData() {
    this.selectedModule = this.monitoring.moduleId;
    this.selectResponsible({ data : this.monitoring ,value : this.monitoring.responsibleId });
    this.selectTester({ data : this.monitoring ,value : this.monitoring.testerId });
  }

  runTest(id?: number) {
    if (id) {
      this.monitoringService.runTest(id);
    }
  }

  openModal(data: any) {

    this.dialogService.open(MonitoringDetailForm, {
      header: data.nameMethodeTest,
      styleClass: 'w-8',
      showHeader: true,
      closable: true,
      maximizable: true,
      resizable: true,
      data: data,
    });
  }
}
