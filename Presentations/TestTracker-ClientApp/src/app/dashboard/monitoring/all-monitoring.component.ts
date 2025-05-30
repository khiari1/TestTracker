import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem, MessageService } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { Monitoring } from 'src/app/models/monitoring.model';
import { MonitoringService } from 'src/app/services/monitoring.service';
import { NotificationService } from 'src/app/services/notification.service';
import { UserService } from 'src/app/services/user.service';
import { CreateJobComponent } from './create-job.component';
import { DialogService } from 'primeng/dynamicdialog';
import { AvatarGenerator } from 'src/app/shared/avatar-generator.service';

interface BreadcrumbLink {
  routerLink: string;
  queryParams?: { [key: string]: any };
  label: string;
}

@Component({
  template: `
    <view-list
      [title]="'Monitoring' | translate"
      [menuItems]="menuItems"
      image="assets/png/icons8-report-file-48.png"
      [(data)]="data"
      [(selectedElement)]="selectedItems"
      [addItem]="'/dashboard/monitoring/create'"
      [editItem]="'/dashboard/monitoring'"
      [dataSource]="'Monitorings'"
    >
      <column-view field="monitoringTestType" [headerText]="'Type'">
        <ng-template let-item>
          <span> {{item.monitoringTestType === 0 ? 'Functionnel':'Unitaire'}} </span>
        </ng-template>
      </column-view>
      <column-view field="moduleName" [headerText]="'Module'"></column-view>   
      <column-view field="testerName" [headerText]="'Tester'">
        <ng-template let-item>
          <tsi-avatar
            [size]="'small'"
            [label]="item.testerName"
            [src]="'https://localhost:7092/Avatar/' + item.testerId + '.png'"
          ></tsi-avatar>
        </ng-template>
      </column-view>
      <column-view field="responcibleName" [headerText]="'Responcible'">
        <ng-template let-item>
          <tsi-avatar
            [size]="'small'"
            [label]="item.responcibleName"
            [src]="
              'https://localhost:7092/Avatar/' + item.responcibleId + '.png'
            "
          ></tsi-avatar>
        </ng-template>
      </column-view>
      <column-view field="nameMethodeTest" [headerText]="'MethodeTest'">
        <ng-template let-item>
          <a
            class="text-blue-300 p-1 hover:surface-hover cursor-pointer"
            [routerLink]="['/dashboard/monitoring/editmonitoring', item.id]"
            routerLinkActive="router-link-active"
          >
            {{ item.nameMethodeTest }}</a
          >
        </ng-template>
      </column-view>
      <column-view width="130px" field="taskState" [headerText]="'Task status'">
        <ng-template let-item>
          <tsi-spinner
            *ngIf="item.taskState == 2"
            label="In Progress..."
          ></tsi-spinner>
          <span *ngIf="item.taskState == 1">Pending</span>
        </ng-template>
      </column-view>
    </view-list>
  `,
  providers: [MessageService, ConfirmationService, DialogService],
})
export class AllMonitoringComponent implements OnInit {
  data: any[] = [];

  selectedItems: Monitoring[] = [];

  monitoring: Monitoring | undefined;

  readPermission: boolean = true;

  readWritePermission: boolean = true;


  menuItems: MenuItem[] = [
    {
      label: 'New job',
      icon: 'pi pi-cog',
      disabled: !this.readWritePermission,
      items: [
        {
          label: 'Run selected test',
          icon: 'pi pi-play',
          command: () => {
            if (this.readPermission) {
              this.runSelectedTest();
            }
          },
    
          disabled: !this.readWritePermission,
        },
        {
          label: 'Add schedular job',
          icon: 'pi pi-calendar',
          command: () => {
            if (this.readPermission) {
              this.addJob(0, 0);
            }
          },
          disabled: !this.readWritePermission,
        },
        {
          label: 'Hourly job',
          icon: 'pi pi-clock',
          command: () => {
            if (this.readPermission) {
              this.addJob(1, 0);
            }
          },
        },
        {
          label: 'Daily job',
          icon: 'pi pi-cog',
          command: () => {
            if (this.readPermission) {
              this.addJob(1, 1);
            }
          },
        },
        {
          label: 'Weekly job',
          icon: 'pi pi-cog',
          command: () => {
            if (this.readPermission) {
              this.addJob(1, 2);
            }
          },
        },
        {
          label: 'Monthly job',
          icon: 'pi pi-cog',
          command: () => {
            if (this.readPermission) {
              this.addJob(1, 3);
            }
          },
        },
      ],
    },
  ];

  constructor(
    protected monitoringService: MonitoringService,
    private NotificationService: NotificationService,
    private _userService: UserService,
    private router: Router,
    private _dialogService: DialogService,
    protected avatarService: AvatarGenerator
  ) {}

  ngOnInit(): void {
    this.readWritePermission =
      this._userService.IsAdmin() ||
      this._userService.hasPermission('Monitoring_ReadWrite');
    this.readPermission =
      this._userService.IsAdmin() ||
      this._userService.hasPermission('Monitoring_Read');

    this.NotificationService.notification.subscribe((notification) => {
      console.log(notification);
      if (notification.area == 'Monitoring') {
        var currentmonitoring = this.data.find((m) => m.id == notification.objectId);
      if (currentmonitoring) {
        currentmonitoring.taskState = notification.value;
      }
      }
      
    });
  }

  add() {
    this.router.navigate(['dashboard/monitoring']);
  }

  runTest(id: number) {
    this.monitoringService.runTest(id);
  }

  runSelectedTest() {
    let selectedIds = this.selectedItems.map((s) => s.id);
    this.monitoringService.runMonitorings(selectedIds);
  }

  addJob(jobMode: any, recuringMode: any) {
    let selectedIds = this.selectedItems.map((s) => s.id);

    let data = {
      hour: 0,
      minute: 0,
      dayOfWeek: 0,
      month: 0,
      jobId: '',
      jobMode: jobMode,
      recuringMode: recuringMode,
      monitoringIds: selectedIds,
    };

    let header = '';
    if (jobMode == 0) {
      header = 'Schedular job';
    } else {
      header = 'Recurring job';
    }

    this._dialogService.open(CreateJobComponent, {
      data: data,
      header: header,
      styleClass: 'w-4',
      resizable: true,
      modal: true,
      keepInViewport: false,
      contentStyle: { overflow: 'visible' },
    });
  }
}
