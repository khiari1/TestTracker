import { Component, OnInit } from '@angular/core';
import {
  DialogService,
  DynamicDialogConfig,
  DynamicDialogRef,
} from 'primeng/dynamicdialog';
import { UserAzureAdModel, UserModel } from 'src/app/models/user.model';
import { GroupService } from 'src/app/services/group.service';

@Component({
  template: `<p *ngIf="users.length === 0">No users to add</p>
            <div *ngFor="let user of users" class="p-2 border-bottom-1 border-300">
              <div class="flex justify-content-between">
                <tsi-avatar
                  [label]="user.userName"
                  [src]="'https://localhost:7092/Avatar/' + user.id + '.png'"
                  [styleClass]="'text-blue-300 hover:surface-hover'"
                ></tsi-avatar>
                <button
                  [hasPermission]="'Group_ReadWrite'"
                  pButton
                  (click)="addUser(user.id)"
                  label="Add user"
                ></button>
              </div>
            </div>
            <div class="p-2">
              <button pButton label="Done" (click)="close()"></button>
            </div>
  `,
})
export class AddUserGroupComponent implements OnInit {
  users: UserAzureAdModel[] = [];
  groupId: string | undefined;

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
      this.groupService.getUserNotInGroup(this.groupId).subscribe((data) => {
        this.users = data;
      });
  }

  addUser(userId: string) {
    if (this.groupId)
      this.groupService.addUser(this.groupId, userId).subscribe((res) => {
        this.loadData();
      });
  }
  close() {
    this.ref.close();
  }
}
