import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { JobService } from 'src/app/services/job.service';
import { Filter, LogicalOperator } from 'src/app/tsing/views/filter.model';

@Component({
  template: `
      <view-list
      [breadcrumbLinks]="breadcrumbLinks"
      [title]="'Processing' | translate"
      image=""
      [(filter)]="filter"
      [dataSource]="'Jobs/Processing'"
    >
    <column-view width="100px" headerText="Id" field="id"></column-view>
        <column-view headerText="Type" field="invocationData.type">
          <ng-template let-item>
            {{ item.invocationData.type }}
          </ng-template>
        </column-view>
        <column-view width="200px" headerText="Started At" field="startedAt">
          <ng-template let-item>
            {{ item.startedAt | date : 'd MMM y, h:mm' }}
          </ng-template>
        </column-view>
    </view-list>
  `,
})
export class ProcessingJobComponent  {
  
  filter: Filter = {
    keyword: '',
    operator: LogicalOperator.And,
    propertyfilters: []
  };

  constructor(protected jobService: JobService) {}

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

}
