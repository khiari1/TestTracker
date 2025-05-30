import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Group } from 'src/app/models/group.model';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-group',
  template: ` <div class="card p-fluid">
    <p-messages></p-messages>
    <div *ngIf="group">
      <div class="field">
        <label for="Name" class="block  font-normal mb-2">Name</label>
        <input
          [(ngModel)]="group.name"
          name="Name"
          id="Name"
          type="text"
          pInputText
          class="w-full mb-3"
        />
      </div>
      <div class="field">
        <label for="lastName" class="block  font-normal mb-2"
          >Description</label
        >
        <input
          [(ngModel)]="group.description"
          name="description"
          id="lastName"
          type="text"
          pInputText
          class="w-full mb-3"
        />
      </div>
      <div class="flex">
        <div class="">
          <button
            [hasPermission]="'Group_ReadWrite'"
            pButton
            
            type="button"
            icon="pi pi-save"
            (click)="save()"
            [loading]="isLoading"
            label="Save"
          ></button>
        </div>
      </div>
    </div>
  </div>`,
  providers: [MessageService],
})
export class AddGroupComponent implements OnInit {
  group: Group = new Group();
  isLoading: boolean = false;
  formState: any = { state: 'Add', id: null };
  id: any;
  constructor(
    private _groupService: GroupService,
    private messageService: MessageService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig
  ) {
    this.formState = config.data;
  }

  ngOnInit(): void {}

  save() {
    this.isLoading = true;
    var observer;
    observer = this._groupService.createGroup(this.group);

    observer?.subscribe({
      next: (data) => {
        this.ref.close(data);
      },
      error: (data) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Info Message',
          detail: JSON.stringify(data.error),
        });
        this.isLoading = false;
      },
    });
  }
}
