import { Component, Input, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'tsi-menu-bar',
  template: `
    <div class="menu-bar-sm">
      <ul class="">
        <li *ngFor="let menuItem of menuItems">
          <a
            class="flex align-items-center menu-item-sm"
            [ngClass]="menuItem.styleClass"
            [routerLink]="menuItem.routerLink"
            (click)="performClick(menuItem)"
          >
            <div class="flex align-items-center font-normal">
              <i [ngClass]="menuItem.icon"></i>
              <span [ngClass]="menuItem.label?'ml-2':''" class="vertical-align-middle">{{ menuItem.label }}</span>
              <i *ngIf="menuItem.items" class="ml-1 pi pi-angle-down"></i>
            </div>
          </a>
          <div
            *ngIf="menuItem.items"
            class="sub-menu-content"
            [ngClass]="menuItem.expanded ? 'expanded' : ''"
          >
            <ul class="sub-menu-item">
              <li *ngFor="let subItem of menuItem.items">
                <a
                  [routerLink]="subItem.routerLink"
                  [queryParams]="subItem.queryParams"
                  (click)="subItem?.command()"
                >
                  <div class="flex align-items-center">
                    <span class=" pr-2" [pTooltip]="subItem.tooltip ?? ''"
                      ><i [class]="subItem.icon"></i
                    ></span>
                    <span>{{ subItem.label }} </span>
                  </div>
                </a>
              </li>
            </ul>
          </div>
        </li>
      </ul>
    </div>
  `,
})
export class MenuBar implements OnInit {
  @Input() menuItems: MenuItem[] = [];

  constructor() {}
  ngOnInit(): void {}

  performClick(menuItem: MenuItem) {
    this.performCommand(menuItem);
    this.toogle(menuItem);
  }

  toogle(menuItem: MenuItem) {
    let expanded = menuItem.expanded;
    this.collapseItems();
    if (menuItem.items) {
      menuItem.expanded = expanded ? false : true;
    }
  }

  performCommand(menuItem: MenuItem) {
    if (menuItem.command) {
      menuItem.command();
    }
  }

  collapseItems(){
    let menuItems = this.menuItems.filter(m =>m.items) 
    menuItems.forEach(item => {
      item.expanded = false;
    })
  }
}
