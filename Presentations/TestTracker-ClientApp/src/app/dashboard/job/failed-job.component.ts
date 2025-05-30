import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { JobService } from 'src/app/services/job.service';
import { Filter, LogicalOperator } from 'src/app/tsing/views/filter.model';

@Component({
  template: `
      <view-list
      [breadcrumbLinks]="breadcrumbLinks"
      [title]="'Fialed job' | translate"
      [menuItems]="items"
      image=""
      [(filter)]="filter"
      [(selectedElement)]="selectedItems"
      [dataSource]="'Jobs/Failed'"
    >
    <column-view width="100px" headerText="Id" field="id"></column-view>
        <column-view headerText="Type" field="invocationData.type">
          <ng-template let-item>
            {{ item.invocationData.type }}
          </ng-template>
        </column-view>
        <column-view headerText="Exception Type" field="exceptionType">
        </column-view>
        <column-view headerText="Exception Message" field="exceptionMessage">
        </column-view>
        <column-view width="200px" headerText="Failed At" field="failedAt">
            <ng-template let-item>
              {{ item.failedAt | date : 'd MMM y, h:mm' }}
            </ng-template>
        </column-view>
    </view-list>

  `,
})
export class FailedJobComponent  {
  
  filter: Filter = {
    keyword: '',
    operator: LogicalOperator.And,
    propertyfilters: []
  };
  
  constructor(protected jobService: JobService) {}

  selectedItems: any[] = [];

  breadcrumbLinks: MenuItem[] = [
    {
      routerLink: '/dashboard',
      label: 'Dashboard',
    },
    {
      routerLink: '/dashboard/job',
      label: 'job',
    },
    {
      routerLink: '/dashboard/job/enqueued-job',
      label: 'Enqueued job',
    },
  ];

  items: MenuItem[] = [
    {
      label: 'Enqueue now',
      icon: 'pi pi-caret-right',
      command : () => this.requeuJobs()
    }
  ];

  async requeuJobs(){
    this.selectedItems?.forEach(element => {
      this.jobService.requeuJobs(element.id)
        .subscribe();
    });
  }
}
