import { Component, OnInit } from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { ResourceService } from 'src/app/services/resource.service';
import { TranslateService } from '@ngx-translate/core';
import { MonitoringService, State } from 'src/app/services/monitoring.service';
import { UserService } from 'src/app/services/user.service';
import { DialogService } from 'primeng/dynamicdialog';
import { MonitoringDetailForm } from './monitoring-detail-form.component';
import { Filter, LogicalOperator, Operator } from 'src/app/tsing/views/filter.model';
import { ActivatedRoute } from '@angular/router';
import { state } from '@angular/animations';


@Component({
  template: `
    <view-list
      [title]="'Monitoring'"
      [menuItems]="items"
      image="assets/png/icons8-test-tube-48.png"
      [(data)]="data"
      [dataSource]="'Monitorings/Details'"
      [(filter)]="filter"
    >
      <column-view width="100px" headerText="State" field="state">
        <ng-template let-item>
          <state-icon [value]="item.state"></state-icon>
        </ng-template>
      </column-view>
      <column-view field="monitoringTestType" [headerText]="'Type'">
        <ng-template let-item>
          <span> {{item.monitoringTestType === 0 ? 'Functionnel':'Unitaire'}} </span>
        </ng-template>
      </column-view>
      <column-view width="200px" headerText="Methode" field="nameMethodeTest">
        <ng-template let-item>
          <span class="ml-2 font-normal text-blue-300 line-height-3 cursor-pointer hover:underline" (click)="openModal(item.id)">
            {{ item.nameMethodeTest }}
          </span>
        </ng-template>
      </column-view>
      <column-view width="200px" headerText="Exception Type" field="exceptionType"></column-view>
      <column-view headerText="Message" field="errorMesage"></column-view>
      <column-view width="200px" headerText="Module" field="moduleName"></column-view>
      <column-view fieldType="date" width="200px" headerText="Date" field="date">
        <ng-template let-item>
          {{ item.date | date : 'd MMM y' }}
        </ng-template>
      </column-view>
      <column-view width="200px" headerText="Duration" field="duration">
        <ng-template let-item>
          <i class="pi pi-clock"></i>
          {{ item.duration}}
        </ng-template>
      </column-view>
    </view-list>

  `,
  providers: [MessageService, DialogService],
})
export class MonitoringDetailsComponent implements OnInit {

  data: any[] = [];
  
  readPermission: boolean = true;

  readWritePermission: boolean = true;

  readFilePermission: boolean = true;

  filter: Filter = {
    keyword: '',
    operator: LogicalOperator.And,
    propertyfilters: [{
      operator : Operator.Equal,
      propertyValue : new Date(Date.now()).toDateString(),
      propertyName : 'Date',
      propertyText : 'Date',
      propertyType : 'date',
    }]
  };


  breadcrumbLinks: MenuItem[] = [
    {
      routerLink: '/dashboard',
      label: 'Dashboard',
    },
    {
      routerLink: '/dashboard/monitoring',
      label: 'Monitoring',
    },
  ];

  items: MenuItem[] = [
    {
      label: 'New monitoring',
      icon: 'pi pi-plus',
      routerLink: '/dashboard/monitoring/editmonitoring',
      styleClass: 'blue-400',
      disabled: !this.readWritePermission
    },
    {
      label: 'Export',
      icon: 'pi pi-file-export',

      command: () => {
        if (this.readFilePermission) {
          this.export();
        }
      },

      disabled: !this.readFilePermission,
    },
  ];

  constructor(
    public translate: TranslateService,
    private _userService: UserService,
    private monitoringService: MonitoringService,
    private _dialogService: DialogService,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit(): void {

    this.route.queryParams.subscribe(q => {

      let properties = Object.keys(q);

      this.filter = {
        keyword: "",
        operator: LogicalOperator.And,
        propertyfilters: []
      };

      properties.forEach(p => {
        this.filter.propertyfilters.push({
          operator : Operator.Equal,
          propertyName : p,
          propertyText : p,
          propertyType : 'text',
          propertyValue : q[p]
        });
      })
      
    })
    this.readWritePermission =
      this._userService.hasPermission('Monitoring_ReadWrite');

    this.readPermission =
      this._userService.IsAdmin() ||
      this._userService.hasPermission('Monitoring_Read');

    this.readFilePermission =
      this._userService.IsAdmin() ||
      this._userService.hasPermission('File_Read');
  }

  export(): void {
    this.monitoringService
      .exportData(this.filter
      )
      .subscribe((response: any) => {
        let blob: Blob = response.body as Blob;
        let url = window.URL.createObjectURL(blob);
        var linkToFile = document.createElement('a');
        linkToFile.download = 'MonitoringDetails';
        linkToFile.href = url;
        linkToFile.click();
      });
  }


  openModal(data: any) {
    this._dialogService.open(MonitoringDetailForm, {
      header: 'Test details',
      styleClass: 'w-8',
      showHeader: true,
      closable: true,
      maximizable: true,
      resizable: true,
      data: data,
    });
  }

}
