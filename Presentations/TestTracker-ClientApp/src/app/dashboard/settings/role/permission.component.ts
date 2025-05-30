import { Component, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { Group } from 'src/app/models/group.model';
import { GroupService } from 'src/app/services/group.service';
import { AddGroupComponent } from './add-group.component';
import { UserService } from 'src/app/services/user.service';
import { UserModel } from 'src/app/models/user.model';
import { MenuItem } from 'primeng/api';
import { group } from '@angular/animations';

@Component({
  template: `
    <div class="p-3  sticky top-0 z-2 surface-0 ">
      <page-header
        [pageTitle]="'Permission'"
        [links]="breadcrumbLinks"
        [iconSrc]="'assets/svg/secure-24.png'"
      >
      </page-header>
    </div>
    <div class="px-3">
      <p-tabView>
        <p-tabPanel class="m-0 p-0" header="Groups">
          <div class="flex justify-content-between">
            <p>
              <p-chip [label]="'Total ' + groups.length"></p-chip>
            </p>
            <button
              [hasPermission]="'Group_ReadWrite'"
              pButton
              
              label="Add group"
              icon="pi pi-plus"
              (click)="add()"
            ></button>
          </div>
          <p-table [value]="groups" styleClass="p-datatable-sm">
            <ng-template pTemplate="header">
              <tr>
                <th>Name</th>
                <th>Description</th>
                <th class="w-1"></th>
              </tr>
            </ng-template>
            <ng-template pTemplate="body" let-group>
              <tr class="cursor-pointer tr-hover">
                <td>
                  <span>
                    <tsi-avatar
                      [src]="
                        'https://localhost:7092/Avatar/' + group.id + '.png'
                      "
                      [label]="group.name"
                      [styleClass]="'text-blue-300 hover:surface-hover'"
                      routerLink="group"
                      [queryParams]="{ id: group.id }"
                    ></tsi-avatar>
                  </span>
                </td>
                <td>{{ group.description }}</td>
                <td>
                  <button
                    pButton
                    class="p-button-text"
                    label="Delete"
                    (click)="delete(group)"
                  ></button>
                </td>
              </tr>
            </ng-template>
          </p-table>
        </p-tabPanel>
        <p-tabPanel header="Users">
          <p-table [value]="users" styleClass="p-datatable-sm">
            <ng-template pTemplate="header">
              <tr>
                <th>Name</th>
                <th>Description</th>
              </tr>
            </ng-template>
            <ng-template pTemplate="body" let-user>
              <tr
                class="cursor-pointer tr-hover"
                routerLink="group"
                [queryParams]="{ id: user.id }"
              >
                <td>
                  <span>
                    <tsi-avatar
                      [label]="user.userName"
                      [src]="'https://localhost:7092/Avatar/' + user.id + '.png'"
                      [styleClass]="'text-blue-300 hover:surface-hover'"
                    ></tsi-avatar>
                  </span>
                </td>
                <td>{{ user.login }}</td>
              </tr>
            </ng-template>
          </p-table>
        </p-tabPanel>
      </p-tabView>
    </div>
  `,
  providers: [DialogService],
})
export class PermissionComponent implements OnInit {
  groups: Group[] = [];
  users: UserModel[] = [];
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
      routerLink: '/dashboard/settings/permissions',
      label: 'Permissions',
    },
  ];
  /**
   *
   */
  constructor(
    private groupService: GroupService,
    private userService: UserService,
    private dialogService: DialogService
  ) {}
  ngOnInit(): void {
    this.loadData();
    this.loadUsers();
  }

  add() {
    this.dialogService.open(AddGroupComponent, {}).onClose.subscribe((resp) => {
      this.groups.push(resp);
    });
  }

  delete(group: Group) {
    this.groupService.delete(group.id ?? '').subscribe((data) => {
      let index = this.groups.indexOf(group);
      this.groups.splice(index, 1);
    });
  }

  loadData() {
    this.groupService.getAllGroups().subscribe((data) => {
      this.groups = data;
    });
  }
  loadUsers() {
    this.userService.getAll().subscribe((data) => {
      this.users = data;
    });
  }
}
