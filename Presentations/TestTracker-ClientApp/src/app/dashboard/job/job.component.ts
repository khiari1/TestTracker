import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { MenuItem } from "primeng/api";

@Component({
  template : `<div class="flex h-full sticky top-0">
  <div class="flex h-full w-16rem p-3">
      <tsi-menu-nav-bar [menuItems]="menuItems" [vertical]="true"></tsi-menu-nav-bar>
  </div>
  <div class="flex-1" style="overflow-y: auto;">
    <router-outlet></router-outlet>
  </div>
</div>`
})
export class JobComponent {

  menuItems: MenuItem[] = [
    {
      label: 'Schedule',
      icon: 'pi pi-calendar',
      routerLink: 'schedule',
    },
    {
      label: 'Recurring',
      icon: 'pi pi-refresh',
      routerLink: 'reccuring',
    },
    {
      label: 'Enqueued',
      icon: 'pi pi-inbox',
      routerLink: 'enqueued',
    },
    {
      label: 'Failed',
      icon: 'pi pi-times',
      routerLink: 'failed',
    },
    {
      label: 'Processing',
      icon: 'pi pi-spin pi-spinner',
      routerLink: 'processing',
    },
    {
      label: 'Succeeded',
      icon: 'pi pi-check',
      routerLink: 'succeeded',
    },
  ];
  constructor(public translate: TranslateService) {

  }
}
