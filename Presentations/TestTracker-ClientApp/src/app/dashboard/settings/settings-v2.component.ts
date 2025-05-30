import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';

type NewType = import('primeng/api').MenuItem;

@Component({
  selector: 'settings-root',
  template: `
    <div class="flex h-full sticky top-0">
      <div class="flex h-full w-14rem p-3">
        <p-menu [model]="settingsMenus"></p-menu>
      </div>
      <div class="flex-1" style="overflow-y: auto;">
        <router-outlet></router-outlet>
      </div>
    </div>
  `,
  providers: [DialogService],
})
export class SettingsV2Component implements OnInit {

  settingsMenus: NewType[] = [
    {
      label: 'Settings',
      items: [
        {
          label: 'General Settings',
          icon: 'pi pi-cog',
          routerLink: 'generalsettings',
        },
        {
          label: this.translate.instant('USERS'),
          icon: 'pi pi-users',
          routerLink: 'users',
        },
        {
          label: this.translate.instant('Permissions_INFO.Permissions'),
          icon: 'pi pi-shield',
          routerLink: 'permissions',
        },

      ],
    },
    {
      label: 'Area',
      items: [
        {
          label: 'Module',
          icon: 'pi pi-list',
          routerLink: 'modules',
        },     
        {
            label: this.translate.instant('Feature'),
          icon: 'pi pi-list',
          routerLink: 'features',
        },
        {
          label: this.translate.instant('Label'),
          icon: 'pi pi-list',
          routerLink: 'labels',
        },
      ],
    },
  ];

  constructor(public translate: TranslateService) {}

  ngOnInit(): void {}
}
