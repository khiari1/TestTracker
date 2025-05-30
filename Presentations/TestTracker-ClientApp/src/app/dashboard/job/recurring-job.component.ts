import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { JobService } from 'src/app/services/job.service';
import { Filter, LogicalOperator } from 'src/app/tsing/views/filter.model';

@Component({
  template: `
    <view-list
      [breadcrumbLinks]="breadcrumbLinks"
      [title]="'Recurring' | translate"
      [menuItems]="items"
      image=""
      [(filter)]="filter"
      [(selectedElement)]="selectedItems"
      [dataSource]="'Jobs/Reccuring'"
    >
      <column-view width="200px" headerText="Id" field="id"></column-view>
      <column-view width="150px" headerText="Cron" field="cron">
        <ng-template let-item>
          <p-tag severity="info" [value]="item.cron"></p-tag>
        </ng-template>
      </column-view>
      <column-view width="150px" headerText="timeZoneId" field="timeZoneId">
        <ng-template let-item>
          {{ item.timeZoneId }}
        </ng-template>
      </column-view>
      <column-view headerText="Job" field="jobName"></column-view>
      <column-view
        width="200px"
        headerText="Next execution"
        field="NextExecution"
      >
        <ng-template let-item>
          {{ item.nextExecution | date : 'd MMM y, h:mm' }}
        </ng-template>
      </column-view>
      <column-view
        width="200px"
        headerText="Last execution"
        field="lastExecution"
      >
        <ng-template let-item>
          <p-tag
            *ngIf="item.lastExecution"
            [severity]="
              item.lastJobState === 'Succeeded' ? 'success' : 'danger'
            "
            [value]="(item.lastExecution | date : 'd MMM y, h:mm') ?? ''"
          ></p-tag>
          <span *ngIf="!item.lastExecution" class="text-italic">N/A</span>
        </ng-template>
      </column-view>
      <column-view width="50px" headerText="Last job id" field="lastJobId">
        <ng-template let-item>
          <span
            *ngIf="item.lastJobId"
            class="ml-2 font-normal text-blue-300 line-height-3 cursor-pointer hover:underline"
          >
            {{ item.lastJobId }}
          </span>
          <span *ngIf="!item.lastJobId"> N/A </span>
        </ng-template>
      </column-view>
    </view-list>
  `,
})
export class RecurringJobComponent {
  filter: Filter = {
    keyword: '',
    operator: LogicalOperator.And,
    propertyfilters: []
  };

  constructor(protected jobService: JobService) {}

  data: any[] = [];
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
      label: 'Trigger now',
      icon: 'pi pi-caret-right',
      command: () => this.trigger(),
    }
  ];

  trigger() {
    let ids = this.selectedItems.map((si) => si.id);
    this.jobService.triggerReccuringJobs(ids).subscribe();
  }
}
