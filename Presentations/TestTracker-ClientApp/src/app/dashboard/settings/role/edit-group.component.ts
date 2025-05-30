import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { Group } from 'src/app/models/group.model';
import { PermissionModel } from 'src/app/models/permission.model';
import { GroupService } from 'src/app/services/group.service';
import { AddUserGroupComponent } from './add-user-group.compoent';
import { UpdateGroupPermissionComponent } from './update-permission-group.component';
import { MenuItem } from 'primeng/api';

@Component({
  template: `
    <div class="p-3  sticky top-0 z-2 surface-0 ">
      <page-header
        [pageTitle]="group?.name + ' | '+ group?.description"
        [links]="breadcrumbLinks"
      >
      <ng-template #titleTemplate>
      <tsi-avatar
            [label]="group?.name +' | ' + group?.description"
            [src]="'https://localhost:7092/Avatar/' + group?.id + '.png'"
            [styleClass]="' text-2xl'"
          ></tsi-avatar> 
      </ng-template>
      </page-header>
    </div>
    <div class="px-3">
    <p-tabView *ngIf="group">
        <p-tabPanel header="Permissions">
          <div class="flex justify-content-end pb-2">
            <button
              [hasPermission]="'Group_ReadWrite'"
              pButton
              
              label="Manage Permission"
              icon="pi pi-plus"
              (click)="UpdatePermission()"
            ></button>
          </div>

          <p-table [value]="grantedPermissions" styleClass="p-datatable-sm">
            <ng-template pTemplate="header">
              <tr>
                <th>Name</th>
                <th>Description</th>
              </tr>
            </ng-template>
            <ng-template pTemplate="body" let-permission>
              <tr>
                <td>
                  {{ permission.key }}
                </td>
                <td>{{ permission.value }}</td>
              </tr>
            </ng-template>
          </p-table>
        </p-tabPanel>

        <p-tabPanel header="Members">
          <div class="flex justify-content-between">
            <p>
              Total
              <p-chip
                [label]="
                  group.userGroups != null
                    ? group.userGroups.length.toString()
                    : '0'
                "
              ></p-chip>
            </p>
            <button
              [hasPermission]="'Group_ReadWrite'"
              pButton
              
              label="Add users"
              icon="pi pi-plus"
              (click)="addUser()"
            ></button>
          </div>

          <p-table [value]="group.userGroups" styleClass="p-datatable-sm">
            <ng-template pTemplate="header">
              <tr>
                <th>Name</th>
                <th>login</th>
                <th></th>
              </tr>
            </ng-template>
            <ng-template pTemplate="body" let-user>
              <tr>
                <td>
                <tsi-avatar
                  [label]="user.userName"
                  [src]="'https://localhost:7092/Avatar/' + user.id + '.png'"
                  [styleClass]="'text-blue-300 hover:surface-hover'"
                ></tsi-avatar>
                </td>
                <td>{{ user.login }}</td>
                <td>
                  <button
                    [hasPermission]="'Group_ReadWrite'"
                    label="remove"
                    class="p-button-secondary p-button-text"
                    pButton
                    (click)="removeUser(user.id)"
                  ></button>
                </td>
              </tr>
            </ng-template>
          </p-table>
        </p-tabPanel>
      </p-tabView>
    </div>
  
  `,
  providers: [DialogService],
})
export class EditGroupComponent implements OnInit {

  group: Group | undefined;

  permisions: PermissionModel[] = [];

  grantedPermissions: PermissionModel[] = [];

  selectedPermission: string[] = [];

  id: any;

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
    {
      routerLink: '/dashboard/settings/permissions/group',
      queryParams : ['id',''],
      label: 'Group',
    },
  ];

  /**
   *
   */
  constructor(
    private groupService: GroupService,
    private activatedroute: ActivatedRoute,
    private dialogService: DialogService
  ) {}
  ngOnInit(): void {

    this.activatedroute.queryParams.subscribe((params) => {
      this.id = params['id'];
      this.breadcrumbLinks = [
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
        {
          routerLink: '/dashboard/settings/permissions/group',
          queryParams : {id: this.id },
          label: 'Group',
        },
      ];
  
      this.loadData();

    });

    
  }

  loadData() {      
    this.groupService.getGroupById(this.id).subscribe((data) => {
        this.group = data;
        this.loadPermission();
        this.groupService.getUserInGroup(this.id).subscribe((data) => {
          this.group!.userGroups = data;
        });
      });

  }
  loadPermission() {
    this.groupService.getPermissions(this.id).subscribe((data) => {
      this.permisions = data;
      this.grantedPermissions = this.permisions.filter(
        (p) => p.selected === true
      );
    });
  }

  UpdatePermission() {
    this.dialogService
      .open(UpdateGroupPermissionComponent, {
        data: this.group?.id,
        styleClass: 'w-4',
        style: '',
      })
      .onClose.subscribe((res) => {
        this.loadData();
      });
  }
  removePermissions() {
    if (this.id)
      this.groupService
        .DeletePermissions(this.id, this.selectedPermission)
        .subscribe();
    this.loadPermission();
  }

  updateSelectedPermissions(permissionKey: string) {
    const index = this.selectedPermission.indexOf(permissionKey);
    if (index > -1) {
      this.selectedPermission.splice(index, 1);
    } else {
      this.selectedPermission.push(permissionKey);
    }
  }

  addUser() {
    this.dialogService
      .open(AddUserGroupComponent, {
        data: this.group?.id,
        styleClass: 'w-4',
        style: '',
      })
      .onClose.subscribe((res) => {
        this.loadData();
      });
  }
  removeUser(userId: string) {
    if (this.group?.id)
      this.groupService.removeUser(this.group.id, userId).subscribe((resp) => {
        this.loadData();
      });
  }
}
