import { AfterViewInit, Component, ContentChild, ContentChildren, Directive, OnInit, Query, QueryList, TemplateRef } from "@angular/core";

let input: string[] = ['allowEditing', 'allowFiltering', 'allowGrouping', 'allowReordering', 'allowResizing', 'allowSearching', 'allowSorting', 'autoFit', 'clipMode', 'columns', 'commands', 'customAttributes', 'dataSource', 'defaultValue', 'disableHtmlEncode', 'displayAsCheckBox', 'edit', 'editTemplate', 'editType', 'enableGroupByFormat', 'field', 'filter', 'filterBarTemplate', 'filterTemplate', 'foreignKeyField', 'foreignKeyValue', 'format', 'formatter', 'freeze', 'headerTemplate', 'headerText', 'headerTextAlign', 'headerValueAccessor', 'hideAtMedia', 'index', 'isFrozen', 'isIdentity', 'isPrimaryKey', 'lockColumn', 'maxWidth', 'minWidth', 'showColumnMenu', 'showInColumnChooser', 'sortComparer', 'template', 'textAlign', 'type', 'uid', 'validationRules', 'valueAccessor', 'visible', 'width'];
let outputs: string[] = ['field'];

@Directive({
  selector : "tsi-grid>tsi-column",
  inputs : input,
  outputs : outputs
})
export class ColumnDirective implements OnInit,AfterViewInit{
  public headerText: string = '';
  public field : any;
  public value : string = '';
  public width : string = '';
  /**
   *
   */
  constructor() {

  }
  ngAfterViewInit(): void {
    console.log(this.template);
  }
  ngOnInit(): void {

  }
  @ContentChild(TemplateRef) template: TemplateRef<any> | undefined;

}

@Directive({
  selector : "ew-grid>ew-columns",
  queries : {
    childColumns: new ContentChildren(ColumnDirective),
  }
})
export class ColumnsDirective {

  /**
   *
   */
  constructor() {

  }

}

