import { Component, Input, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'tsi-menu-nav-bar',
  template: `
    <div [ngClass]="{ 'menu-bar': true, 'menu-bar-vertical': vertical, 'menu-bar-horizontal': !vertical }">
      <ul>
        <li *ngFor="let menuItem of menuItems">
          <a class="flex align-items-center" [routerLink]="menuItem.routerLink" routerLinkActive="active">
            <div class="flex align-items-center">
                <i class="mr-2" [ngClass]="menuItem.icon"></i> <span class="vertical-align-middle">{{ menuItem.label }}</span>
            </div>
          </a>
        </li>
      </ul>
    </div>
  `,
})
export class MenuNavBar implements OnInit {
  @Input() menuItems: MenuItem[] = [];
  @Input() vertical:boolean = false;

  constructor() {}
  ngOnInit(): void {}
}

