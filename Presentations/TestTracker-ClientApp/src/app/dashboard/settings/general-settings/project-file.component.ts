import { Component, ElementRef, ViewChild } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ProjectFileService } from 'src/app/services/project-file.service';
@Component({
    template: `
      <!-- #region Headder-->
      <div class="py-5">
      <p-messages></p-messages>
      <div class=" text-2xl">Upload Project file</div>
    </div>
    <!-- #endregion -->
    <!-- #endregion -->
    <div class="my-1">
    <div class="formgrid p-fluid" *ngIf="data">
          <div class="field">
            <label for="minuteInterval">{{ 'Project name' | translate }}</label>
            <input
              pInputText
              type="text"
              id="minuteInterval"
              [(ngModel)]="data.projectName"
            />
          </div>
          <div class="field">
            <label for="minuteInterval">{{ 'File name' | translate }}</label>
            <input
              pInputText
              type="text"
              id="minuteInterval"
              readonly
              [(ngModel)]="data.fileName"
            />
          </div>
        </div>
        <div class="pb-1">
        <button
          pButton
          icon="pi pi-plus"
          label="Upload .zip"
          (click)="selectFile()"
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
    </div>
    <div class="flex flex-row-reverse">
        <p-button [label]="'cancel'" (onClick)="ref.close(data)"></p-button>
        <p-button [label]="'save'" (onClick)="save()" styleClass="mr-1 p-button-text"></p-button>
        
    </div>
  `,
})
export class ProjectFileComponent {

    
    data: any = {};
    selectedFile?: File;

    @ViewChild('inputFile') inputFile: ElementRef | undefined;

    /**
     *
     */
    constructor(protected projectFileService : ProjectFileService,protected ref : DynamicDialogRef ,protected conf : DynamicDialogConfig) {
    }
    
    fileChange(event: any): void {
        let fileList: FileList = event.target.files;
        console.log(fileList);
        if (fileList.length > 0) {
            this.selectedFile = fileList[0];
        }
    }
    
    selectFile() {
        this.inputFile?.nativeElement.click();
    }
    
    save() {
        if (this.selectedFile) {
            this.projectFileService
              .Upload(this.selectedFile,this.data.projectName)
              .subscribe();
          }
    }
}
