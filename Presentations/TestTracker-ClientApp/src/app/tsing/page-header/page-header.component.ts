import {
  ChangeDetectionStrategy,
  Component,
  ContentChild,
  Input,
  TemplateRef,
} from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'page-header',
  template: `
    <div class="pb-1">
      <div class="mb-1 flex justify-content-between">
        <div class="flex">
          <div class="pr-2" *ngIf="iconSrc">
            <img 
              width="24px" 
              height="24px" 
              [src]="iconSrc" 
              alt="" />
          </div>
          <div *ngIf="titleHeaderTemplate" class="p-datatable-header">
            <ng-container *ngTemplateOutlet="titleHeaderTemplate"> </ng-container>
          </div>
          <div *ngIf="titleTemplate" class="p-datatable-header">
            <ng-container *ngTemplateOutlet="titleTemplate"> </ng-container>
          </div>
          <div *ngIf="!(titleHeaderTemplate || titleTemplate)" class=" text-2xl">{{ pageTitle }}</div>
        </div>
        <div class="flex" *ngIf="rightHeaderTemplate">
          <ng-container *ngTemplateOutlet="rightHeaderTemplate"></ng-container>
        </div>
        <div class="flex" *ngIf="right">
          <ng-container *ngTemplateOutlet="right"></ng-container>
        </div>
        <tsi-menu-bar [menuItems]="menuItems"></tsi-menu-bar>
    </div>
      </div>
  `,
  changeDetection : ChangeDetectionStrategy.OnPush,
})
export class PageHeaderComponent {
  
  @Input() links: MenuItem[] = [];

  @Input() iconSrc: string = '';

  @Input() pageTitle: string = '';

  @Input() menuItems!: MenuItem[];


  @Input() public rightHeaderTemplate! : TemplateRef<any>;

  @Input() public titleHeaderTemplate! : TemplateRef<any>;

  @ContentChild("titleTemplate") titleTemplate!: TemplateRef<any>;
  @ContentChild("right") right!: TemplateRef<any>;



}
