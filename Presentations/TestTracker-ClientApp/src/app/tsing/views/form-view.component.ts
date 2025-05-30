import {
  Component,
  ContentChild,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
  TemplateRef,
} from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { TranslateService } from '@ngx-translate/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { HttpClientService } from 'src/app/shared/http-service.service';

@Component({
  selector: 'form-view',
  template: `
    <!-- #region Headder-->
    <div class="p-3 pb-0  sticky top-0 z-2 surface-0 ">
      <p-messages [enableService]="true"></p-messages>
      <page-header
        [links]="breadcrumbLinks"
        [iconSrc]="image"
        [pageTitle]="title"
        [menuItems]="menuItems"
        [rightHeaderTemplate]="rightHeaderTemplate"
        [titleHeaderTemplate]="titleTemplate"
      >
      </page-header>
    </div>
    <!-- #endregion -->
    <!-- #endregion -->
    <div class="mx-3">
        <ng-container *ngTemplateOutlet="content"></ng-container>
      </div>

    
  `,
  providers: [MessageService,HttpClientService],
})
export class FormViewComponent implements OnInit, OnChanges {

  @Input() dataSource: string | undefined;

  @Input() breadcrumbLinks: MenuItem[] = [];

  @Input() image: string = '';

  @Input() title: string = '';

  @Input() menuItems: MenuItem[] = [];

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
      command: () => this.loadData(),
    },
  ];

  private isLoding: boolean = true;

  @Input() data: any;

  @Output() dataChange: EventEmitter<any> = new EventEmitter<any>();

  @Input() id: any;

  @ContentChild('rightHeaderTemplate') rightHeaderTemplate!: TemplateRef<any>;

  @ContentChild('titleTemplate') titleTemplate!: TemplateRef<any>;

  @ContentChild('content') content!: TemplateRef<any>;

  @Input()
  formStatus: 'edit' | 'create' = 'create';

  @Output()
  formStatusChange : EventEmitter<'edit' | 'create'> = new EventEmitter<'edit' | 'create'>();

  /**
   *
   */
  constructor(
    protected translation: TranslateService,
    protected router: ActivatedRoute,
    protected route: Router,
    protected httpClient: HttpClientService,
    protected messageService: MessageService
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data'] && this.isLoding === false) {
      this.defaultMenuItems[0].disabled = false;
    }
  }

  ngOnInit(): void {
    if (this.dataSource) this.httpClient.setEndpoints(this.dataSource);

    this.menuItems.push(...this.defaultMenuItems);

    this.dataChange.subscribe(() => {
      this.httpClient.calculate(this.data);
    });

    this.activatedRoute();

    this.initStatus();

    this.loadData();
  }

  initStatus() {
    if (this.id) {
      this.formStatus = 'edit';
      this.formStatusChange.emit(this.formStatus);
    }
  }

  activatedRoute() {
    this.router.params.subscribe((data) => {
      if (data['id'] != 'create') {
        this.id = data['id'];
      }
    });
  }

  loadData() {
    if (this.formStatus === 'edit') {
      this.httpClient.getById(this.id).subscribe((data) => {
        this.data = data;
        this.dataChange.emit(data);
        this.isLoding = false;
      });
    } else {
      this.data = new Object();
      this.dataChange.emit(this.data);
    }
  }

  save() {
    if (this.formStatus === 'edit') {
      this.httpClient.update(this.id, this.data).subscribe({
        next: () => {
          this.messageService.add({ severity: 'info', detail: 'Chages saved with success' });
          this.dataChange.emit(this.data);
          
          
        },
        error: (err: HttpErrorResponse) => {
          console.log(err);
          console.log(JSON.stringify(err.error.Errors));
          this.messageService.add({ severity: 'error', detail: err.error.errors != undefined ? JSON.stringify(err.error.errors) : err.error});
        },
      });
    } else {
      this.httpClient.create(this.data).subscribe({
        next: (data) => {
          this.formStatus = 'edit'
          this.formStatusChange.emit(this.formStatus);
          this.data = data
          this.dataChange.emit(this.data);
        },
        error: (err: HttpErrorResponse) => {
          
          this.messageService.add({ severity: 'error', detail: err.error.errors != undefined ? JSON.stringify(err.error.errors) : err.error });
        },
      });
    }
  }

  itemClick(event: any, item: MenuItem) {
    if(item.command)
      item.command?.(event);
    if (item.routerLink) {
      this.route.navigate([item.routerLink]);
    }
  }
}
