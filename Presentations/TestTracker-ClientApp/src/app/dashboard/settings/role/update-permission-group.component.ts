import { Component, OnInit } from '@angular/core';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PermissionModel } from 'src/app/models/permission.model';
import { GroupService } from 'src/app/services/group.service';

@Component({
  template: `
    <p *ngIf="permissions.length === 0">No permissions to add</p>
    <div
      *ngFor="let permission of permissions!"
      class="p-2 border-bottom-1 border-300"
    >
      <div class="grid">
        <div class="col">
          {{ permission!.key }} :
          {{ permission.value }}
        </div>
        <div class="col-fixed" style="width:100px">
          <div class="col-fixed" style="width:100px">
            <p-checkbox
              [(ngModel)]="selectedKeys"
              [value]="permission.key"
            ></p-checkbox>
            <!-- <button
              pButton
              (click)="permission.key && updatePermission([permission.key])"
              label="Add permission"
            ></button> -->
          </div>
        </div>
      </div>
    </div>
    <div class="p-2">
      <button pButton label="Done" (click)="updatePermission()"></button>
    </div>
  `,
})
export class UpdateGroupPermissionComponent implements OnInit {
  permissions: PermissionModel[] = [];
  selectedKeys : string[]= [];
  groupId!: string;
  constructor(
    private groupService: GroupService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig
  ) {}

  ngOnInit(): void {
    this.groupId = this.config.data;
    this.loadData();
  }
  loadData() {
    if (this.groupId)
      this.groupService.getPermissions(this.groupId).subscribe((data) => {
        this.permissions = data;
        this.selectedKeys = this.permissions.filter(p => p.selected === true).map(p =>p.key??'')
      });
  }

  updatePermission() {
      this.groupService
        .updatePermission(this.groupId, this.selectedKeys)
        .subscribe({
          next : ()=>{
              this.ref.close();
          }
        });
  }

}
