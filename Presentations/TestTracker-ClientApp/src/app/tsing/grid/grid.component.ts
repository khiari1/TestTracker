import {
  AfterViewInit,
  ChangeDetectionStrategy,
  Component,
  ContentChildren,
  EventEmitter,
  Input,
  OnInit,
  Output,
  QueryList,
} from '@angular/core';
import { ColumnDirective } from './columns.directive';

@Component({
  selector: 'tsi-grid',
  template: `
    <div class="border-top-1 border-300"></div>
    <p-table
      [value]="data"
      styleClass="p-datatable-sm"
      [tableStyle]="{ 'min-width': '50rem' }"
      selectionMode="multiple"
      [(selection)]="selectedItems"
      (selectionChange)="selectedItemsChange.emit(selectedItems)">
    >
      <ng-template pTemplate="header">
        <tr>
          <th width="4rem" *ngIf="multiSelect">
            <p-tableHeaderCheckbox></p-tableHeaderCheckbox>
          </th>
          <th *ngFor="let column of columns" [width]="column.width">
            {{ column.headerText }}
          </th>
        </tr>
      </ng-template>
      <ng-template pTemplate="body" let-item let-rowIndex="rowIndex">
        <tr
          [ngClass]="
            isSelected(item)
              ? 'p-element p-hilieght p-selectable-row p-highlight'
              : ''
          "
        >
          <td *ngIf="multiSelect">
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
            <span *ngIf="!column.template">
              {{ item[column.field] }}
            </span>
          </td>
        </tr>
      </ng-template>
    </p-table>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GridComponent implements OnInit, AfterViewInit {
  @Input() data: any[] = [];

  @Input() selectionMode!: string;

  @Input() multiSelect: boolean = false;

  @Input() selectedItems: any[] = [];

  @Output() selectedItemsChange: EventEmitter<any> = new EventEmitter<any>();

  @ContentChildren(ColumnDirective) columns!: QueryList<ColumnDirective>;

  ngAfterViewInit(): void {}

  ngOnInit(): void {}

  isSelected(item: any): boolean {
    return this.selectedItems.find((s) => s === item) !== undefined;
  }
}
