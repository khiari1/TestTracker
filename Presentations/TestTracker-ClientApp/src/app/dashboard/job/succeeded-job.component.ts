import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { JobService } from 'src/app/services/job.service';
import { Filter, LogicalOperator } from 'src/app/tsing/views/filter.model';

@Component({
  template: `

<view-list
      [breadcrumbLinks]="breadcrumbLinks"
      [title]="'Succeeded' | translate"
      [menuItems]="items"
      image=""
      [(filter)]="filter"
      [(selectedElement)]="selectedItems"
      [dataSource]="'Jobs/Succeeded'"
    >
    <column-view width="100px" headerText="Id" field="id"></column-view>
        <column-view headerText="Type" field="invocationData.type">
          <ng-template let-item>
            {{ item.invocationData.type }}
          </ng-template>
        </column-view>
        <column-view
          width="200px"
          headerText="Total duration"
          field="totalDuration"
        >
          <ng-template let-item>
            {{ item.totalDuration | date : 'd MMM y, h:mm' }}
          </ng-template>
        </column-view>
        <column-view width="200px" headerText="Succeeded">
          <ng-template let-item>
            {{ item.succeededAt | date : 'd MMM y, h:mm' }}
          </ng-template>
        </column-view>
    </view-list>


  `,
})
export class SucceededJobComponent {

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
      id : 'enqueue-now',
      label: 'Enqueue now',
      icon: 'pi pi-caret-right',
      command: () => this.requeuJobs(),
    }
  ];

  requeuJobs() {
    let ids = this.selectedItems.map(si => si.id);
      this.jobService.requeuJobs(ids).subscribe();
  }
}
