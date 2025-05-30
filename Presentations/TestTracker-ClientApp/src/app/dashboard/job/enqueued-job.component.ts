import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { MenuItem } from 'primeng/api';
import { JobService } from 'src/app/services/job.service';
import { Filter, LogicalOperator } from 'src/app/tsing/views/filter.model';

@Component({
  template: `
    <view-list
      [breadcrumbLinks]="breadcrumbLinks"
      [title]="'Enqueued job' | translate"
      image=""
      [(filter)]="filter"
      [dataSource]="'Jobs/Enqueued'"
    >
      <column-view width="100px" headerText="Id" field="id"></column-view>
      <column-view width="200px" headerText="EnqueueAt" field="enqueueAt">
        <ng-template let-item>
          {{ item.enqueueAt | date : 'd MMM y, h:mm' }}
        </ng-template>
      </column-view>
      <column-view headerText="Type" field="invocationData.type">
        <ng-template let-item>
          {{ item.invocationData.type }}
        </ng-template>
      </column-view>
      <column-view width="200px" headerText="Scheduled At" field="scheduledAt">
        <ng-template let-item>
          {{ item.scheduledAt | date : 'd MMM y, h:mm' }}
        </ng-template>
      </column-view>
    </view-list>
  `,
})
export class EnqueuedJobComponent implements OnInit {
  /**
   *
   */
  constructor(
    protected jobService: JobService,
    private translateService: TranslateService
  ) {}


  filter: Filter = {
    keyword: '',
    operator: LogicalOperator.And,
    propertyfilters: []
  };

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

  ngOnInit(): void {
    
  }

}
