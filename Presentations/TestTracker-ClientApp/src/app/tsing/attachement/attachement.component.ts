import { Component, ElementRef, OnInit, ViewChild, Input } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Attachement } from 'src/app/models/file.model';
import { FileService } from 'src/app/shared/file.service';

@Component({
  selector: 'app-attachement',
  template: `
    <div class="py-4">
      <button
        pButton
        icon="pi pi-plus"
        label="Add Attachement"
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
    <p-table
      [value]="files"
      dataKey="id"
      styleClass="p-datatable-sm"
      selectionMode="single"
    >
      <ng-template pTemplate="header">
        <tr>
          <th pSortableColumn="fileName">
            {{ 'File Name' | translate }}
            <p-sortIcon field="fileName"></p-sortIcon>
          </th>
          <th width="200px" pSortableColumn="fileSize">
            {{ 'File Size' | translate }}
            <p-sortIcon field="fileSize"></p-sortIcon>
          </th>
          <th width="200px" pSortableColumn="userId">
            {{ 'User' | translate }}
            <p-sortIcon field="userId"></p-sortIcon>
          </th>
          <th width="150px" pSortableColumn="date">
            {{ 'Date' | translate }}
            <p-sortIcon field="date"></p-sortIcon>
          </th>
          <th width="100px"></th>
        </tr>
      </ng-template>
      <ng-template pTemplate="body" let-item>
        <tr>
          <td>
            <span class=""><i class="pi pi-file pr-2"></i> {{ item.fileName }}</span>
          </td>
          <td>
            {{ item.fileSize }}
          </td>
          <td>
            {{ item.userId }}
          </td>
          <td>
            <i class="pi pi-calendar"></i>
            {{ item.date | date : 'dd/MM/yyyy' }}
          </td>
          <td>
            <button
              pButton
              icon="pi pi-download"
              class=" p-button-text p-button-primary pr-2"
              (click)="downloadFile(item.fileName)"
            ></button>
            <button
              pButton
              icon="pi pi-trash"
              class=" p-button-text p-button-danger"
              (click)="deleteFile(item.fileName)"
            ></button>
          </td>
        </tr>
      </ng-template>
    </p-table>
  `,
  providers: [MessageService],
})
export class AttachementComponent implements OnInit {
  @Input() keyGroup: string | undefined;

  @Input() objectId: number | undefined;

  file: any;

  files: Attachement[] = [];

  @ViewChild('inputFile') inputFile: ElementRef | undefined;

  constructor(
    private FileService: FileService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    if (this.keyGroup === undefined || this.objectId === undefined) {
      throw new Error('requered pram ');
    }
    this.FileService.getFiles(this.keyGroup, this.objectId).subscribe(
      (data) => {
        this.files = data;
      }
    );
  }

  upload() {
    if (this.keyGroup && this.objectId)
      this.FileService.upload(
        this.file,
        this.keyGroup,
        this.objectId
      ).subscribe((res) => {
        this.loadData();

        this.file = null;
      });
  }

  fileChange(event: any) {
    let fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      this.file = fileList[0];
    }

    this.upload();
  }

  downloadFile(fileName: string) {
    if (this.keyGroup === undefined || this.objectId === undefined) {
      throw new Error('requered pram ');
    }
    this.FileService.download(fileName, this.keyGroup, this.objectId).subscribe(
      (data) => {
        console.log(data)
        let blob = new Blob([data], { type: 'application/octet-stream' });
        console.log(blob);
        const downloadURL = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = downloadURL;
        link.download = fileName; // Set the desired file name
        link.click();
      }
    );
  }

  deleteFile(fileName: string) {
    if (this.keyGroup === undefined || this.objectId === undefined) {
      throw new Error('requered pram ');
    }
    this.FileService.delete(this.keyGroup, this.objectId,fileName).subscribe();
  }

  selectFile() {
    this.inputFile?.nativeElement.click();
  }
}
