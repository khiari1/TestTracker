import {
  AfterContentInit,
  Component,
  ContentChild,
  ContentChildren,
  EventEmitter,
  Input,
  OnInit,
  Output,
  QueryList,
  TemplateRef,
  Type,
} from '@angular/core';
import { ConfirmationService, MenuItem, MessageService } from 'primeng/api';
import { ColumnViewDirective } from './column-view.directive';
import { TranslateService } from '@ngx-translate/core';
import {
  Filter,
  LogicalOperator,
  Operator,
  Propertyfilter,
} from './filter.model';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { HttpClientService } from 'src/app/shared/http-service.service';
import { HttpErrorResponse } from '@angular/common/http';
import { config } from 'rxjs';

@Component({
  selector: 'view-list',
  template: `    
    <p-confirmDialog></p-confirmDialog>
    <!-- #region Headder-->
    <div class="p-3  sticky top-0 z-2 surface-0 ">
      <p-messages></p-messages>
      <page-header
        [links]="breadcrumbLinks"
        [iconSrc]="image"
        [pageTitle]="title"
        [menuItems]="_menuItems"
        [rightHeaderTemplate]="rightHeaderTemplate"
        [titleHeaderTemplate]="titleTemplate"
      >
      </page-header>

      <div class="flex justify-content-between">
        <div class="flex flex-1 justify-item-center">
          <div>
            <span class="p-input-icon-left mr-1">
              <i class="pi pi-search"></i>
              <input
                type="text"
                pInputText
                placeholder="Search"
                [(ngModel)]="filter.keyword"
                (ngModelChange)="filterChange.emit(filter)"
              />
            </span>
          </div>
          <div class="flex flex-wrap" *ngIf="allowSearchFields">
            <div *ngFor="let item of filter.propertyfilters" class="pr-1 pb-1">
              <p-chip [removable]="true" (onRemove)="removeFilter(item)" (click)="op.toggle($event)" styleClass="cursor-pointer surface-200">
                <div class="p-chip-text">
                  <span class="font-bold">{{ item.propertyName }}</span>
                  <span *ngIf="item.operator === 0"> == </span>
                  <span *ngIf="item.operator === 1"> != </span>
                  <span *ngIf="item.operator === 2"> CONTAINS </span>
                  <span *ngIf="item.operator === 3"> > </span>
                  <span *ngIf="item.operator === 4"> >= </span>
                  <span *ngIf="item.operator === 5"> < </span>
                  <span *ngIf="item.operator === 6"> <= </span>
                  <span
                    class="ml-2 "
                    *ngIf="item.propertyType == 'date'"
                    >{{ item.propertyValue | date : 'd MMM y' }}</span
                  >
                  <span
                    class="ml-2 "
                    *ngIf="item.propertyType != 'date'"
                    >{{ item.propertyValue }}</span
                  >
                </div>
              </p-chip>
            </div>
            <p-chip (click)="op.toggle($event)"
              label="Add filter"
              icon="pi pi-filter"
              class="cursor-pointer">
              </p-chip>
           <!--<p-button
              (click)="openFilterView()"             
              label="save filter"
              icon="pi pi-filter"
              styleClass="mr-1"
            >
            </p-button>            
            <p-splitButton
              icon="pi pi-filter"
              (onClick)="setFilter('info')"
              [model]="filterStoreMenuItems"
              >
            </p-splitButton>-->
            <p-overlayPanel #op [style]="{ width: '250px' }">
              <ng-template pTemplate>
                <div class="p-1">
                  <div class="grid">
                    <div class="col">
                      <p class="text-2xl">{{ 'Add filter' | translate }}</p>
                    </div>
                  </div>
                  <div class="grid">
                    <div class="col">
                      <div class="p-fluid">
                        <div class="field">
                          <p-dropdown
                            [(ngModel)]="selectedProperty"
                            [options]="filtrableProperties"
                            optionLabel="propertyText"
                          ></p-dropdown>
                        </div>
                        <div class="field">
                          <p-dropdown
                            [(ngModel)]="selectedProperty.operator"
                            [options]="filterOperator"
                            optionLabel="label"
                            optionValue="operator"
                          ></p-dropdown>
                        </div>
                        <div class="field">
                          <p-calendar
                            *ngIf="selectedProperty.propertyType == 'date'"
                            [(ngModel)]="selectedProperty.propertyValue"
                          ></p-calendar>
                          <input
                            *ngIf="selectedProperty.propertyType != 'date'"
                            type="text"
                            [(ngModel)]="selectedProperty.propertyValue"
                            pInputText
                            placeholder="Search"
                          />
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="flex justify-content-end">
                    <p-button
                      (onClick)="op.toggle($event)"
                      [label]="'Cancel' | translate"
                      styleClass="mr-1 p-button-secondary"
                    ></p-button>
                    <p-button
                      (onClick)="addFilter()"
                      (onClick)="op.toggle($event)"
                      [label]="'Apply' | translate"
                      styleClass=""
                    ></p-button>
                  </div>
                </div>
              </ng-template>
            </p-overlayPanel>
          </div>
        </div>
        <div class="flex">
        <p-chip
            (onRemove)="clearGroup()"
            [removable]="true"
            *ngIf="groupColumn"
            styleClass="mr-1"
          >
          <div class="p-chip-text"><i class="pi pi-list font-bold"></i><span class="font-bold"> Grouped by </span>{{groupColumn.headerText}}</div></p-chip>
          <p-chip
            (onRemove)="clearSelectedElement()"
            [removable]="true"
            *ngIf="selectedElement != undefined && selectedElement.length > 0"
            styleClass="mr-1"
            [label]="'Selected element ' + selectedElement.length"
          ></p-chip>
          <p-chip [label]="'Total ' + data.length"></p-chip>
          <tsi-menu-bar [menuItems]="groupByItems"></tsi-menu-bar>
        </div>
      </div>
    </div>
    <!-- #endregion -->

    <!-- #region Table-->
    <div
      class="px-3"
      style="position: relative; bottom: 0px; overflow-y: auto; height: max-content;"
    >
      <p-table
        *ngIf="!groupColumn"
        [value]="data"
        styleClass="p-datatable-sm"
        [tableStyle]="{ 'min-width': '50rem' }"
        [selectionMode]="selectionMode"
        [(selection)]="selectedElement"
        [resizableColumns]="true"
        (selectionChange)="selectedElementChange.emit(selectedElement)"
        (onColResize)="writeSize($event)"
      >
        <ng-template pTemplate="header">
          <tr>
            <th width="4rem" *ngIf="selectionMode === 'multiple'">
              <p-tableHeaderCheckbox></p-tableHeaderCheckbox>
            </th>
            <th
              pResizableColumn
              *ngFor="let column of columns"
              [width]="column.width"
              [pSortableColumn]="column.field"
            >
              <p-sortIcon [field]="column.field"></p-sortIcon>
              {{ column.headerText | translate }}
            </th>
          </tr>
        </ng-template>
        <ng-template pTemplate="body" let-item let-rowIndex="rowIndex">
          
          <tr *ngIf="!isLoading"
            [ngClass]="{ 'p-highlight': isSelectedRow(item) }"
            (dblclick)="editItemAction($event, item)"
            class="cursor-pointer"
          >
            <td *ngIf="selectionMode === 'multiple'">
              <p-tableCheckbox [value]="item"></p-tableCheckbox>
            </td>
            <td *ngFor="let column of columns">
              <span *ngIf="column.template">
                <ng-container
                  *ngTemplateOutlet="
                    column.template;
                    context: { $implicit: item }
                  "
                ></ng-container>
              </span>
              <span *ngIf="!column.template">{{ item[column.field] }}</span>
            </td>
          </tr>
        </ng-template>
      </p-table>
        
      <p-table
        *ngIf="groupColumn"
        [value]="data"
        styleClass="p-datatable-sm"
        [tableStyle]="{ 'min-width': '50rem' }"
        [selectionMode]="selectionMode"
        [(selection)]="selectedElement"
        [resizableColumns]="true"
        (selectionChange)="selectedElementChange.emit(selectedElement)"
        (onColResize)="writeSize($event)"
        [dataKey]="groupColumn!.field"
        rowGroupMode="subheader"
        [groupRowsBy]="groupColumn!.field"
      >
        <ng-template pTemplate="header">
          <tr>
            <th width="4rem" *ngIf="selectionMode === 'multiple'">
              <p-tableHeaderCheckbox></p-tableHeaderCheckbox>
            </th>
            <th
              pResizableColumn
              *ngFor="let column of columns"
              [width]="column.width"
              [pSortableColumn]="column.field"
            >
              <p-sortIcon [field]="column.field"></p-sortIcon>
              {{ column.headerText | translate }}
            </th>
          </tr>
        </ng-template>
        <ng-template
          pTemplate="groupheader"
          let-item
          let-rowIndex="rowIndex"
          let-expanded="expanded"
        >
          <tr>
            <td [colSpan]="columns.length + 1">
              <div class="flex align-items-center">
              <button
                type="button"
                pButton
                [pRowToggler]="item"
                class="p-button-text p-button-rounded p-button-plain mr-2"
                [icon]="expanded ? 'pi pi-chevron-down' : 'pi pi-chevron-right'"
              ></button>
              <span class="font-bold pr-2">({{groupTotal(groupColumn.field,item[groupColumn.field])}})</span>
              <span class="" *ngIf="groupColumn.template">
                <ng-container
                  *ngTemplateOutlet="
                    groupColumn.template;
                    context: { $implicit: item }
                  "
                ></ng-container>
              </span>
              <span class="" *ngIf="!groupColumn.template">{{ item[groupColumn.field] }}</span>              
              </div>
            </td>
          </tr>
        </ng-template>
        <ng-template pTemplate="rowexpansion" let-item>
          <tr
            [ngClass]="{ 'p-highlight': isSelectedRow(item) }"
            (dblclick)="editItemAction($event, item)"
            class="cursor-pointer"
          >
            <td *ngIf="selectionMode === 'multiple'">
              <p-tableCheckbox [value]="item"></p-tableCheckbox>
            </td>
            <td *ngFor="let column of columns">
              <span *ngIf="column.template">
                <ng-container
                  *ngTemplateOutlet="
                    column.template;
                    context: { $implicit: item }
                  "
                ></ng-container>
              </span>
              <span *ngIf="!column.template">{{ item[column.field] }}</span>
            </td>
          </tr>
        </ng-template>
      </p-table>  
    </div>
    <!-- #endregion -->
    <!-- #region Loader indicator-->
          <div class="flex justify-content-center pt-5">
        <tsi-spinner label="Loading..." *ngIf="isLoading"></tsi-spinner>
      </div> 
    <!-- #endregion -->
  `,
  providers: [DialogService, MessageService, HttpClientService,ConfirmationService],
})
export class ViewListComponent implements OnInit, AfterContentInit {


  @Input() editItem: any | undefined;

  @Input() addItem: any | undefined;

  @Input() dataSource: string = '';

  @Input() breadcrumbLinks: MenuItem[] = [];

  @Input() image: string = '';

  @Input() title: string = '';

  @Input() menuItems: MenuItem[] = [];

  @Input() selectedElement: any[] = [];

  @Input() allowSearchFields: boolean = true;

  @Input() data: any[] = [];

  @Input() selectionMode: string = 'multiple';

  @Output() filterChange: EventEmitter<any> = new EventEmitter<any>();

  @Output() onRowDbClick: EventEmitter<any> = new EventEmitter<any>();

  @Output() selectedElementChange: EventEmitter<any> = new EventEmitter<any>();

  @Output() dataChange: EventEmitter<any[]> = new EventEmitter<any[]>();

  protected _menuItems: MenuItem[] = [];

  defaultMenuItems: MenuItem[] = [
    {
      label: 'Clear Filters',
      icon: 'pi pi-filter-slash',
      command: () => this.clearFilter(),
    },
    {
      label: 'Clear selection',
      icon: 'pi pi-circle',
      command: () => this.clearSelectedElement(),
    },
  ];

  @ContentChild('rightHeaderTemplate') rightHeaderTemplate!: TemplateRef<any>;

  @ContentChild('titleTemplate') titleTemplate!: TemplateRef<any>;

  @ContentChildren(ColumnViewDirective)
  columns!: QueryList<ColumnViewDirective>;

  groupColumn: ColumnViewDirective | undefined;

  filterStore: any[] = [];

  selectedProperty: Propertyfilter = {
    propertyText: '',
    propertyName: '',
    operator: Operator.Equal,
    propertyValue: '',
    propertyType: '',
  };

  filterStoreMenuItems: MenuItem[] = [];

  filterValue: any;

  filtrableProperties: Propertyfilter[] = [];

  filterOperator: any[] = [
    {
      operator: Operator.Equal,
      label: '==',
    },
    {
      operator: Operator.NotEqual,
      label: '!=',
    },
    {
      operator: Operator.Contains,
      label: 'Contains',
    },
    {
      operator: Operator.GreaterThan,
      label: '>',
    },
    {
      operator: Operator.GreaterThanOrEqual,
      label: '>=',
    },
    {
      operator: Operator.LessThan,
      label: '<',
    },
    {
      operator: Operator.LessThanOrEqual,
      label: '<=',
    },
  ];

  selectedFilterOperator: any;

  groupByItems :MenuItem[]=[]

  private _filter: Filter | undefined;
  @Input() set filter(value: Filter) {
    this._filter = value;
    this.filterChange.emit(this._filter);
  }

  get filter(): Filter {
    if (this._filter == undefined) {
      this._filter = {
        keyword: '',
        operator: LogicalOperator.And,
        propertyfilters: [],
      }
    }
    return this._filter;
  }

  protected isLoading: boolean = false;
  /**
   *
   */
  constructor(
    protected translation: TranslateService,
    protected activateRouter: ActivatedRoute,
    protected router: Router,
    protected dialogService: DialogService,
    protected httpclientService: HttpClientService,
    protected messageService: MessageService,
    protected confirmationService: ConfirmationService
  ) { }
  
  ngAfterContentInit(): void {
    if (this.columns) {
      this.columns
        .filter((c) => c.allowSearching === true)
        .forEach((c) => {
          this.filtrableProperties.push({
            propertyName: this.capitalize(c.field),
            propertyText: c.headerText
              ? this.translation.instant(c.headerText)
              : '',
            propertyType: c.fieldType,
            operator: Operator.Equal,
            propertyValue: '',
          });
        });
    }

    if (this.addItem) {
      this._menuItems.push({
        label: 'New',
        icon: 'pi pi-plus',
        command: () => this.addItemAction(),
        styleClass:"bg-green-600 text-white",
      });
    }

    this._menuItems.push(...this.menuItems);

    this._menuItems.push({
      label: 'Delete',
      icon: 'pi pi-trash',
      command: (event) => this.deleteItemAction(event),
    });

    if (this.dataSource) {
      this._menuItems.push({
        label: 'Refresh',
        icon: 'pi pi-sync',
        command: () => this.loadData(),
      });
    }

    let groupByMneu: MenuItem = { label: 'Group by',icon:'pi pi-bars', items: [] };
    this.columns.forEach((item) => {
      groupByMneu.items!.push({
        label: item.headerText,
        command: () => this.groupBy(item.field),
      });
    });

    this._menuItems.push(...this.defaultMenuItems);
    this.groupByItems.push(groupByMneu);

    if (this.dataSource) {
      this.httpclientService.setEndpoints(this.dataSource);
    }
    this.loadData();
  }

  ngOnInit(): void {
    this.activateRouter.queryParams.subscribe(q => {

      let properties = Object.keys(q);

      this.filter = {
        keyword: "",
        operator: LogicalOperator.And,
        propertyfilters: []
      };

      properties.forEach(p => {
        let values = q[p] as Array<any>
        if (typeof(values) === 'object') {
          values.forEach(v => {
            this.filter.propertyfilters.push({
              operator: Operator.Equal,
              propertyName: p,
              propertyText: p,
              propertyType: 'text',
              propertyValue: v
            });
          })
        } else {
          this.filter.propertyfilters.push({
            operator: Operator.Equal,
            propertyName: p,
            propertyText: p,  
            propertyType: 'text',
            propertyValue: q[p]
          });
        }

        
      });
    });

    this.filterChange.subscribe(() => {
      this.loadData();
    });
    this.loadFilter();
  }

  loadFilter() {
    this.httpclientService.setEndpoints('QueryStores');
    this.httpclientService.get(
      {
        keyword: '',
        operator: LogicalOperator.And,
        propertyfilters: [
          {
            operator: Operator.Equal,
            propertyName: 'Area',
            propertyText: 'Area',
            propertyType: 'String',
            propertyValue: this.dataSource
          }]
      }).subscribe(query => {
      this.filterStore = query;
      this.filterStoreMenuItems = this.filterStore.map<MenuItem>(i =>  ({ label: i.name , command : () => this.setFilter(i)}))
      
    });
    this.httpclientService.setEndpoints(this.dataSource);
  }

  openFilterView() {
    this.dialogService.open(FilterFormView, {
      styleClass: 'w-3',
      showHeader: false, data: { data: { name: 'Test', area: this.dataSource, isShared: true, query: this._filter } }
    });
  }
  saveFilter() {
    this.httpclientService.setEndpoints('QueryStores');
    this.httpclientService.create({ name: 'Test',area : this.dataSource, isShared: true, query: this._filter })
      .subscribe();
      this.httpclientService.setEndpoints(this.dataSource);
  }  

  setFilter(filter : any) {
    this.filter = filter.query;
  }

  addFilter() {
    this.filter.propertyfilters.push({
      propertyName: this.selectedProperty.propertyName,
      operator: this.selectedProperty.operator,
      propertyValue: this.selectedProperty.propertyValue,
      propertyText: this.selectedProperty.propertyText,
      propertyType: this.selectedProperty.propertyType,
    });
    this.filterChange.emit(this.filter);
  }

  removeFilter(filter: any) {
    this.filter.propertyfilters.splice(
      this.filter.propertyfilters.indexOf(filter),
      1
    );
    this.filterChange.emit(this.filter);
  }
  capitalize(str: string) {
    return str?.replace(/^\w/, (c) => c.toUpperCase());
  }

  clearSelectedElement() {
    this.selectedElement = [];
    this.selectedElementChange.emit(this.selectedElement);
  }

  deleteSelectedElement() {
    this.selectedElement.forEach(i => {
      this.data.splice(this.data.indexOf(i), 1);
    });

    this.clearSelectedElement();
  }

  clearFilter() {
    this.filter = {
      keyword: '',
      operator: LogicalOperator.And,
      propertyfilters: [],
    };
    this.filterChange.emit(this.filter);
  }

  isSelectedRow(item: any) {
    return this.selectedElement.find((d) => d.id === item.id) !== undefined;
  }

  navigate(id?: string) {
    if (id) this.router.navigate([this.editItem, id]);
    else this.router.navigate([this.addItem]);
  }

  openForm(form: Type<any>, id?: any) {
    let result = this.dialogService.open(form, {
      data: { id  : id},
      styleClass: 'w-3',
      showHeader: false,
    });
    result.onClose.subscribe((item) => {
      if (item.id) {
        let Updateditem = this.data.find((i) => i.id == item.id);
        if (Updateditem) Updateditem = item;
        else {
          this.data.push(item);
        }
      }
    });
  }

  loadData() {
    this.isLoading = true;
    this.httpclientService.get(this.filter).subscribe((data) => {
      this.data = data;
      this.dataChange.emit(this.data);
      this.isLoading = false
    });
  }

  addItemAction() {
    if (typeof this.addItem === 'string') {
      this.navigate();
    } else if (typeof this.addItem === 'function') {
      this.openForm(this.editItem);
    }
  }

  deleteItemAction(event: any) {
    let ids = this.selectedElement.map(s => s.id);
    this.confirm(() =>
      this.httpclientService.batchDelete(ids).subscribe({
        next: () => {
          this.deleteSelectedElement();
        },
        error: (err: HttpErrorResponse) => {
          this.messageService.add({ severity: 'error', detail: err.message });
        },
      }));
  }

  editItemAction($event: any, item: any) {
    this.onRowDbClick.emit({ event: $event, item: item });
    if (typeof this.editItem === 'string') {
      this.navigate(item.id);
    } else if (typeof this.editItem === 'function') {
      this.openForm(this.editItem, item.id);
    }
  }

  confirm(action: any) {
    this.confirmationService.confirm({
      message: 'Do you want to delete selected records?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      acceptIcon: 'none',
      rejectIcon: 'none',
      rejectButtonStyleClass: 'p-button-text',
      accept: () => {
        action();
      },
      reject: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Rejected',
          detail: 'You have rejected',
          life: 3000,
        });
      },
    });
  }

  groupBy(field: string) {
    this.groupColumn = this.columns.find((c) => c.field == field);
  }
  clearGroup() {
    this.groupColumn = undefined;
    }

  groupTotal(field: string, value: any): number {
    return this.data.filter(d => d[field] == value).length;
  }

  writeSize($event: any) {
    console.log($event);
  }
}

@Component({
  template: `
  <modal-form-view
    [dataSource]="'QueryStores'"
    [title]="'Query'"
    [(data)]="data"
  >
    <ng-template #content>
    <div class="formgrid p-fluid" *ngIf="data">
        <div class="field">
          <label for="name">{{ 'Name' | translate }}</label>
          <input
            pInputText
            [(ngModel)]="data.name"
            id="name"
            type="text"
            />
        </div>
      </div>
    </ng-template>
  </modal-form-view>`  
})
export class FilterFormView {
  
  @Input() data: any;

}
