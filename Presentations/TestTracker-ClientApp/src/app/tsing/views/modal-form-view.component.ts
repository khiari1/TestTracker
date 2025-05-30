import {
  Component,
  ContentChild,
  EventEmitter,
  Input,
  OnInit,
  Output,
  TemplateRef,
} from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { TranslateService } from '@ngx-translate/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { HttpClientService } from 'src/app/shared/http-service.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'modal-form-view',
  template: `
    <!-- #region Headder-->
    <div class="py-5">
      <p-messages></p-messages>
      <div *ngIf="!(titleTemplate)" class=" text-2xl">{{ title }}</div>
      <div *ngIf="titleTemplate" class="p-datatable-header">
            <ng-container *ngTemplateOutlet="titleTemplate"> </ng-container>
          </div>
    </div>
    <!-- #endregion -->
    <!-- #endregion -->
    <div class="my-1">
      <ng-container *ngTemplateOutlet="content"></ng-container>
    </div>
    <div class="flex justify-content-end mt-3">
        <tsi-menu-bar [menuItems]="menuItems"></tsi-menu-bar>
    </div>
  `,
  providers : [HttpClientService,MessageService]
})
export class ModalFormViewComponent implements OnInit {

  @Input() breadcrumbLinks: MenuItem[] = [];

  @Input() image: string = '';

  @Input() title: string = '';

  @Input() menuItems: MenuItem[] = [];

  @Input() data: any;

  @Input() dataSource: string = '';
  
  @Output() dataChange: EventEmitter<any> = new EventEmitter<any>;

  @Input() id: any;

  get isModal(): boolean {
    return this.conf?.data != undefined; 
  };

  defaultMenuItems: MenuItem[] = [
    {
      label: 'Save',
      icon: 'pi pi-check',
      styleClass: 'bg-green-600 text-white',
      command: () => this.save(),
    },
    {
      label: 'Discard',
      icon: 'pi pi-times',
      command: () => this.ref?.close(),
    },
  ];

  @ContentChild('rightHeaderTemplate') rightHeaderTemplate!: TemplateRef<any>;

  @ContentChild('titleTemplate') titleTemplate!: TemplateRef<any>;

  @ContentChild('content') content!: TemplateRef<any>;

  formStatus: 'edit' | 'create'  = 'create';
  /**
   *
   */
  constructor(
    protected translation: TranslateService,
    protected router: ActivatedRoute,
    protected route: Router,
    protected httpClient: HttpClientService,
    protected messageService : MessageService,
    protected ref?: DynamicDialogRef ,
    protected conf?: DynamicDialogConfig,
    
  ) { }
  
  ngOnInit(): void {

    this.menuItems.push(...this.defaultMenuItems);
    this.id = this.conf?.data?.id;
    this.data = this.conf?.data?.data;
    if (this.data) {
      this.dataChange.emit(this.data);
    }

    this.httpClient.setEndpoints(this.dataSource);

    this.initStatus();

    this.loadData();
  }
  
  initStatus() {
    if (this.id) {
      this.formStatus = 'edit'
    }
  }

  loadData() {
    if (this.formStatus === 'edit') {
      this.httpClient.getById(this.id).subscribe(data => {
        this.data = data;
        this.dataChange.emit(data);
      });
    } else if(this.data == undefined) {
      this.data = new Object();
      this.dataChange.emit(this.data);
    }256
  }

  save() {
    if (this.formStatus === 'edit') {
      
      this.httpClient.update(this.id,this.data).subscribe({
        next: (data) => {
          this.messageService.add({ severity: 'info', detail: 'Chages saved with success' });
          this.data = data;
          this.dataChange.emit(this.data);
          this.ref?.close(this.data);
          
        },
        error: (err: HttpErrorResponse) => {
          console.log(err);
          console.log(JSON.stringify(err.error.Errors));
          this.messageService.add({ severity: 'error', detail: this.errorPrettier(err)});
        },
      });
    } else {
      this.httpClient.create(this.data).subscribe(data => {
        this.data = data;
        this.dataChange.emit(data);
        this.ref?.close(data)
      });
    }
  }

  errorPrettier(err: HttpErrorResponse) : string {
    
    if (err.error.errors != undefined) {
      let errorMessage: string = '';
      (err.error.errors as Array<any>).forEach((e): any => {
        errorMessage += e.toString() + '\n';
      });
      return errorMessage;
    } else {
      return err.error;
    }
  }
}
