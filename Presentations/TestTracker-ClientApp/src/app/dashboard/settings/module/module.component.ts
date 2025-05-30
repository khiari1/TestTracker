import { Component } from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { ConfirmationService } from 'primeng/api';
import { ModuleFormComponent } from './module-form.component';

@Component({
  template: `

<view-list
  [title]="'Module' | translate"
  [breadcrumbLinks]="breadcrumbLinks"
  [allowSearchFields]="false"
  [dataSource]="'Modules'"
  [editItem]="form"
  [addItem]="form"
>
  <column-view headerText="Id" field="id" [allowSearching]="false" width="50px">
    <ng-template let-item>
      <span class="text-blue-300 p-1 hover:surface-hover cursor-pointer"
      >{{item.id}}</span>
    </ng-template>
  </column-view>
  <column-view headerText="Name" field="name"></column-view>
</view-list>

  `,
})
export class ModuleComponent  {
  /**
   *
   */
  breadcrumbLinks: MenuItem[] = [
    {
      routerLink: '/dashboard',
      label: 'Dashboard',
    },
    {
      routerLink: '/dashboard/settings',
      label: 'Settings',
    },
    {
      routerLink: '/dashboard/settings/modules',
      label: 'Modules',
    },
  ];
  form: any = ModuleFormComponent;

}
