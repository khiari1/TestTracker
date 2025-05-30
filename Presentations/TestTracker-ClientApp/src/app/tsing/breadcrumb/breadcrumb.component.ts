import { Component, OnInit } from '@angular/core';
import { Breadcrumb, BreadcrumbService } from './breadcrumb.service';
import { Router } from '@angular/router';

@Component({
  selector: 'tsi-breadcrumb',
  template: `
    <div class="font-normal ml-2 mr-2">
      <ul class="list-none flex align-items-center">
        <li
          *ngFor="
            let link of breadcrumbItem;
            let last = last;
            let first = first
          "
          class="border-round p-1 hover:surface-hover cursor-pointer"
        >
          <a
            [routerLink]="link.url"
            class="breadcrumb-link text-color"
            ngClass=" no-underline  text-xl"
          >
            {{ link.label }}
          </a>
          <span *ngIf="link.queryParams"><a [routerLink]="link.url" target="_blank">#{{link.queryParams}}</a></span>
          <span class="ml-2 mr-2" *ngIf="!last"> / </span>
        </li>
      </ul>
    </div>
  `,
})
export class BreadcrumbMenu implements OnInit {
  breadcrumbItem: Breadcrumb[] = [];
  constructor(private breadcrumb: BreadcrumbService, private router: Router) {
    this.breadcrumb.breadcrumbs$.subscribe((b) => (this.breadcrumbItem = b));
  }
  ngOnInit(): void {}

  private navigate() {}
}
