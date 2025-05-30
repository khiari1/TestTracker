import { Component, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { UserService } from 'src/app/services/user.service';
import { TranslateService } from '@ngx-translate/core';
import { InviteUserComponent } from './invite-user.component';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { AvatarGenerator } from 'src/app/shared/avatar-generator.service';

interface BreadcrumbLink {
  routerLink: string;
  queryParams?: { [key: string]: any };
  label: string;
}
@Component({
  selector: 'app-user',
  template: `
    <view-list
      [title]="'Users' | translate"
      [breadcrumbLinks]="breadcrumbLinks"
      [allowSearchFields]="true"
      [menuItems]="items"
      [dataSource]="'v1/Users'"
      [image]="'assets/png/icons8-user-48.png'"
      [editItem]="'dashboard/settings/users'"
    >
      <column-view
        headerText="Display Name"
        field="userName"
        [allowSearching]="true"
      >
        <ng-template let-item>
          <tsi-avatar
            [size]="'small'"
            [label]="item.userName"
            [src]="'https://localhost:7092/Avatar/' + item.id + '.png'"
            [styleClass]="'text-blue-300 hover:surface-hover'"

          ></tsi-avatar>
        </ng-template>
      </column-view>
      <column-view
        headerText="User principal name"
        field="userPrincipalName"
      ></column-view>
      <column-view headerText="Mail" field="mail"></column-view>
      <column-view headerText="Status" field="subMenuName">
        <ng-template let-item>
          <p-tag
            *ngIf="item.accountEnabled"
            severity="success"
            value="Enbled"
          ></p-tag>
          <p-tag
            *ngIf="!item.accountEnabled"
            severity="danger"
            value="Disabled"
          ></p-tag>
        </ng-template>
      </column-view>
      <column-view
        headerText="Creation type"
        field="creationType"
      ></column-view>
    </view-list>
  `,
  providers: [DialogService],
})
export class UserComponent implements OnInit {

  syncloader: boolean = false;

  readPermission: boolean = true;

  readWritePermission: boolean = true;

  breadcrumbLinks: BreadcrumbLink[] = [
    {
      routerLink: '/dashboard',
      label: 'Dashboard',
    },
    {
      routerLink: '/dashboard/settings',
      label: 'Settings',
    },
    {
      routerLink: '/dashboard/settings/users',
      queryParams: { id: 1 },
      label: 'Users',
    },
  ];

  items: MenuItem[] = [
    {
      label: 'New user',
      icon: 'pi pi-plus',
      items: [
        {
          label: 'Add user',
          command: () => {
            if (this.readWritePermission) {
              this.add();
            }
          },
          icon: 'pi pi-plus',
          disabled: !this.readWritePermission,
        },
        {
          label: 'Invite user',
          command: () => {
            if (this.readWritePermission) {
              this.invitUser();
            }
          },
          icon: 'pi pi-plus',
          disabled: !this.readWritePermission,
        },
      ],
    },
    {
      label: 'Syncronize from AzureAD',
      command: () => {
        if (this.readWritePermission) {
          this.synchronizeUserFromAzureAD();
        }
      },
      icon: 'pi pi-sync',
      disabled: !this.readWritePermission,
    },
  ];


  constructor(
    private _userService: UserService,
    private _dialogService: DialogService,
    public translate: TranslateService,
    private router: Router,
    protected generator: AvatarGenerator
  ) { }
  
  ngOnInit(): void {

    this.readWritePermission =
      this._userService.IsAdmin() ||
      this._userService.hasPermission('User_ReadWrite');
    this.readPermission =
      this._userService.IsAdmin() ||
      this._userService.hasPermission('User_Read');
  }

  add() {
    this.router.navigate(['dashboard/settings/users/create']);
  }

  synchronizeUserFromAzureAD() {
    this.syncloader = true;
    this._userService.synchronizeUserFromAzureAD().subscribe((data) => {
      this.syncloader = false;
    });
  }

  invitUser() {
    this._dialogService.open(InviteUserComponent, {
      header: 'New User',
      styleClass: 'w-3',
    });
  }
}
