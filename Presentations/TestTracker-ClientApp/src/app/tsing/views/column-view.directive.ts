import { AfterViewInit, ContentChild, Directive, Input, OnInit, TemplateRef } from "@angular/core";

@Directive({
    selector : "view-list>column-view"
  })
  export class ColumnViewDirective {

    @Input() public headerText!: string ; 

    @Input() public fieldType : string = 'string';

    @Input() public field : any;

    @Input() public value! : string;

    @Input() public width! : string;
  
    @Input() allowSearching : boolean = true;
    
    @Input() allowSorting : boolean = false;
  
    @Input() textAlign : string ='left'
  
    /**
     *
     */
    constructor() {
  
    }

    @ContentChild(TemplateRef) template: TemplateRef<any> | undefined;
  
  }