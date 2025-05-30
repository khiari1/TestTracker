import { Component, OnInit } from "@angular/core";
import { MessageService } from "primeng/api";
import { DynamicDialogConfig, DynamicDialogRef } from "primeng/dynamicdialog";
import { UserService } from "src/app/services/user.service";

@Component({
  template: `
    <p-messages></p-messages>
    <div class="card">
      <div class="formgrid">
        <div class="field">
          <label for="name">{{ 'User Email Address' | translate }}</label>
          <input
            pInputText
            [(ngModel)]="invitedUserEmailAddress"
            id="invitedUserEmailAddress"
            type="text"
            class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
          />
        </div>
        <div class="field">
          <label for="name">{{ 'User Display Name' | translate }}</label>
          <input
            pInputText
            [(ngModel)]="userDisplayName"
            id="userDisplayName"
            type="text"
            class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
          />
        </div>
        <div class="field">
          <label for="name">{{ 'User First Name' | translate }}</label>
          <input
            pInputText
            [(ngModel)]="userFirstName"
            id="userFirstName"
            type="text"
            class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
          />
        </div>
        <div class="field">
          <label for="name">{{ 'User las Name' | translate }}</label>
          <input
            pInputText
            [(ngModel)]="userLastName"
            id="userLastName"
            type="text"
            class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
          />
        </div>
        <div class="field">
          <form class="flex align-items-center gap-1">
            <p-checkbox value="SendMessage" inputId="ny"></p-checkbox>
            <label for="name">{{
              'Send Invitation Message' | translate
            }}</label>
          </form>
        </div>
      </div>
      <button
        [hasPermission]="'User_ReadWrite'"
        pButton
        
        type="button"
        icon="pi pi-save"
        class="p-button-success"
        (click)="save()"
        [loading]="isLoading"
        label="{{ 'Save_BTN' | translate }}"
      ></button>
    </div>
  `,
  providers: [MessageService],
})
export class InviteUserComponent implements OnInit {
  invitedUserEmailAddress!: string;
  isLoading: boolean = false;
  userDisplayName!: string;
  userFirstName!: string;
  userLastName!: string;
  SendMessage: boolean = true;
  /**
   *
   */
  constructor(
    private userService: UserService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private messageService: MessageService
  ) {}
  ngOnInit(): void {}

  save() {
    this.isLoading = true;
    this.userService
      .InviteUser(
        this.invitedUserEmailAddress,
        this.userFirstName,
        this.userLastName,
        this.userDisplayName,
        this.SendMessage
      )
      .subscribe({
        next: () => {
          this.isLoading = false;
          this.ref.close();
        },
        error: (data) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Info Message',
            detail: data.message,
          });
          this.isLoading = false;
        },
      });
  }
}
