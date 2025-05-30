import { Component, OnInit } from "@angular/core";
import { ConfirmationService, MessageService } from "primeng/api";
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from "primeng/dynamicdialog";
import { AzureDevopsSettings, Team } from "src/app/models/general-settings.model";

import { GeneralSettingsService } from "src/app/services/generalSettings.service";

@Component({
  selector: 'app-module-form',
  template: `
    <div class="card" *ngIf="team">
      <div class="field">

        <label for="Name">{{ 'Name' | translate }}</label>
        <input
          [(ngModel)]="team.teamName"
          id="name"
          type="text"
          class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
        />

      </div>
      <button
        pButton
        
        type="button"
        icon="pi pi-save"
        class="p-button-success"
        label="{{ 'Save_BTN' | translate }}"
        (click)="save()"


      ></button>
    </div>
  `,
  providers: [MessageService, ConfirmationService],
})
export class TeamSettingsComponent implements OnInit {
  azuredevops: AzureDevopsSettings = new AzureDevopsSettings();
  team: Team = new Team();
  isLoading: boolean = false;
  formState: any;
  observer: any;

  constructor(
    public _generalSettings : GeneralSettingsService,
    private messageService: MessageService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    public dialogService: DialogService
  ) {

  }

  ngOnInit(): void {


  }
  save() {
    var observer;
    observer = this._generalSettings.addSettings(this.team,"Team").subscribe({
      next: (resp:any) => {
        this.messageService.add({
          severity: 'info',
          summary: 'Info Message',
          detail: 'succesfuly saved',
        });
          this.ref.close();
      },
      error: (data:any) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Info Message',
          detail: data.message,
        });

      },


        });
  }
}
