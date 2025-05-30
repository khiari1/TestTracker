import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { TranslateService } from '@ngx-translate/core';
import { MsalService } from '@azure/msal-angular';
import { AvatarGenerator } from 'src/app/shared/avatar-generator.service';
import { ThemeService } from 'src/app/services/theme.service';

@Component({
  selector: 'app-header-menu',
  template: `
    <div
      class="header surface-100 border-bottom-1 border-200 text-color"
      style="z-index:1;"
    >
      <div class="p-2 flex justify-content-between align-items-center">
        <div class="flex align-items-center">
          <div class="flex align-items-center cursor-pointer" routerLink="/">
            <img width="32px" height="32px" src="assets/svg/icon.svg" />
            <p class="mx-3 text-2xl">Tsi testtracker</p>
          </div>
          <tsi-breadcrumb></tsi-breadcrumb>
        </div>
        <div class="flex align-items-center">
          <tsi-menu-bar [menuItems]="menuItems"></tsi-menu-bar>
          <tsi-notificationPanel></tsi-notificationPanel>
          <tsi-avatar
            *ngIf="user"
            class="flex cursor-pointer"
            (click)="op.toggle($event)"
            [label]="user.userName"
            [src]="'https://localhost:7092/Avatar/' + user.id + '.png'"
          ></tsi-avatar>
        </div>
      </div>
      <tsi-menu-nav-bar [menuItems]="items"></tsi-menu-nav-bar>
    </div>
    <p-overlayPanel #op>
      <ng-template pTemplate>
        <div class="p-2">
          <div class="pb-2 flex align-items-center">
            <tsi-avatar
              [src]="'https://localhost:7092/Avatar/' + user.id + '.png'"
            ></tsi-avatar>
            <div class="flex flex-column">
              <span class="text-2xl bold">{{ user?.userName }}</span>
              <span class="">{{ user?.mail }}</span>
            </div>
          </div>
          <div>
            <button
              pButton
              type="button"
              class="p-button-text w-full mb-1"
              (click)="me()"
              label="Edit"
              icon="pi pi-user-edit"
            ></button>
            <button
              pButton
              type="button"
              class="p-button-text w-full mb-1"
              (click)="logout()"
              label="Logout"
              icon="pi pi-power-off"
            ></button>
          </div>
        </div>
      </ng-template>
    </p-overlayPanel>
  `,
})
export class HeaderMenuComponent implements OnInit {
  // Inputs
  @Input() user?: any | null;

  // Properties
  items: MenuItem[] = [];
  menuItems: MenuItem[] = [
    {
      icon: 'pi pi-power-off',
      command: () => this.logout(),
    },
    {
      icon: 'pi pi-sun',
      command: () => this.themeService.darkLightMode(),
    },
  ];

  // Constructor
  constructor(
    private msalService: MsalService,
    private router: Router,
    public translate: TranslateService,
    public avatarService: AvatarGenerator,
    public themeService: ThemeService
  ) {
    translate.setDefaultLang('fr');
  }

  // Lifecycle Hooks
  ngOnInit(): void {
    this.initializeMenuItems();
  }

  // Methods
  private initializeMenuItems(): void {
    this.items = [
      {
        label: 'Dashboard',
        routerLink: 'widget',
        icon: 'pi pi-play',
      },
      {
        label: 'Job',
        routerLink: 'job/reccuring',
        icon: 'pi pi-play',
      },
      {
        label: 'Monitoring',
        routerLink: 'monitoring',
        icon: 'pi pi-desktop',
      },
      {
        label: 'Monitoring detail',
        routerLink: 'monitoring/detail',
        icon: 'pi pi-check-circle',
      },
      {
        label: 'Settings',
        routerLink: 'settings',
        icon: 'pi pi-cog',
      },
    ];
  }

  logout(): void {
    this.msalService.logout();
  }

  me(): void {
    this.router.navigate(['dashboard/settings/me']);
  }
}
