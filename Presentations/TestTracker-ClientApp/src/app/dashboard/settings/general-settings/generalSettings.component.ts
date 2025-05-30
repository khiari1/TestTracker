import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { MenuItem } from 'primeng/api';
import {
  AzureDevopsSettings,
  GeneralSettings,
  Project,
  Team,
} from 'src/app/models/general-settings.model';
import { GeneralSettingsService } from 'src/app/services/generalSettings.service';
import { DialogService } from 'primeng/dynamicdialog';
import { ProjectFileService } from '../../../services/project-file.service';
import { ProjectFileComponent } from './project-file.component';

@Component({
  selector: 'app-user',
  template: `
    <div class="p-3  sticky top-0 z-2 surface-0 ">
      <page-header
        [pageTitle]="'General settings'"
        [links]="breadcrumbLinks"
        [iconSrc]="'assets/png/icons8-gear-32.png'"
      >
        <ng-template #right>
          <button
            pButton
            label="Save"
            [icon]="'pi pi-save'"
            (click)="save()"
          ></button>
        </ng-template>
      </page-header>
    </div>
    <div class="px-3">
      <div class="flex">
        <img width="20px" src="assets/svg/azure.svg" alt="azure DevOps" />
        <span class="ml-2 font-bold">Azure DevOps</span>
      </div>
      <p-messages
        [value]="[
          {
            severity: 'info',
            summary: 'Info',
            detail:
              'A personal access token contains your security credentials for Azure DevOps. A PAT identifies you, your accessible organizations, and scopes of access. As such, theyre as critical as passwords, so you should treat them the same way.'
          }
        ]"
        [enableService]="false"
        [closable]="false"
      ></p-messages>
      <div class="grid">
        <div class="col-4">
          <div class="p-fluid">
            <div class="field">
              <label for="minuteInterval">{{ 'PAT' | translate }}</label>
              <input
                pInputText
                type="text"
                id="minuteInterval"
                [(ngModel)]="azuredevops.pat"
              />
            </div>
            <div class="field">
              <label for="minuteInterval">{{
                'Project Name' | translate
              }}</label>
              <input
                pInputText
                type="text"
                id="minuteInterval"
                [(ngModel)]="project.projectName"
              />
            </div>
            <div class="field">
              <label for="minuteInterval">{{ 'Team Name' | translate }}</label>
              <input
                pInputText
                type="text"
                id="minuteInterval"
                [(ngModel)]="team.teamName"
              />
            </div>
          </div>
        </div>
      </div>
      <div class="flex pb-2">
        <!-- <img width="24px" src="assets/svg/azure.svg" alt="azure DevOps" /> -->
        <span class="ml-2 font-bold">Test project</span>
      </div>
      <p-messages
        [value]="[
          {
            severity: 'info',
            summary: 'Info',
            detail:
              'Create a test project with NUnit and Selunium and run solution, zip files in release folder and upload.\n\r'
          }
        ]"
        [enableService]="false"
        [closable]="false"
      ></p-messages>
      <p-messages
        [value]="[
          {
            severity: 'warn',
            summary: 'Warning',
            detail:
              'Supported plateforme .net core with c# language.\n\rThe test project to upload must be with c#, Nunit and selinum framework.'
          }
        ]"
        [enableService]="false"
        [closable]="false"
      ></p-messages>
      <div class="pb-1">
        <button
          pButton
          icon="pi pi-plus"
          label="Upload .zip"
          (click)="addProjectFile()"
        ></button>
        <input
          style="display: none;"
          #inputFile
          pInputText
          class="pb-2"
          type="file"
          placeholder="choose file"
          (change)="fileChange($event)"
        />
      </div>
      <div class="grid" *ngIf="testProjectInfo">
        <div class="col-4">
          <div class="p-fluid">
            <div class="field">
              <label for="minuteInterval">{{
                'Project name' | translate
              }}</label>
              <input
                pInputText
                type="text"
                id="minuteInterval"
                [(ngModel)]="testProjectInfo.projectName"
              />
            </div>
            <div class="field">
              <label for="minuteInterval">{{ 'File name' | translate }}</label>
              <input
                pInputText
                type="text"
                id="minuteInterval"
                [(ngModel)]="testProjectInfo.fileName"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
  providers: [DialogService],
})
export class GeneralSettingsComponent implements OnInit {
  constructor(
    public translate: TranslateService,
    public _generalSettings: GeneralSettingsService,
    private projectFileService: ProjectFileService,
    private dialogService : DialogService
  ) {}

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
      routerLink: '/dashboard/settings/generalsettings',
      label: 'General Settings',
    },
  ];
  settings?: any;

  setting: GeneralSettings[] = [];

  file!: File;

  name!: string;

  azuredevops: AzureDevopsSettings = new AzureDevopsSettings();

  currentTabHeader?: string;

  project: Project = new Project();

  team: Team = new Team();

  testProjectInfo?: any;

  @ViewChild('inputFile') inputFile: ElementRef | undefined;

  selectFile() {
    this.inputFile?.nativeElement.click();
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this._generalSettings.getSettings('AzureDevops').subscribe((data) => {
      this.azuredevops = data;
    });

    this.projectFileService.get().subscribe((data) => {
      this.testProjectInfo = data;
    });
  }

  save() {
    if (this.team) {
      this._generalSettings.addSettings(this.team, 'Team').subscribe();
    }
    if (this.project) {
      this._generalSettings.addSettings(this.project, 'Project').subscribe();
    }
    if (this.azuredevops) {
      this._generalSettings
        .addSettings(this.azuredevops, 'AzureDevops')
        .subscribe();
    }
    
  }

  fileChange(event: any): void {
    let fileList: FileList = event.target.files;
    console.log(fileList);
    if (fileList.length > 0) {
      this.file = fileList[0];
    }
  }

  addProjectFile() {
    let result = this.dialogService.open(ProjectFileComponent, {
      styleClass: 'w-3',
      showHeader: false,
    });
  }
}
