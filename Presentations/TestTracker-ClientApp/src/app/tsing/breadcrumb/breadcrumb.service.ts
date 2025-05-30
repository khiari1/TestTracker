// src/app/services/breadcrumb.service.ts
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, NavigationEnd, Router } from '@angular/router';
import { BehaviorSubject, filter } from 'rxjs';

export interface Breadcrumb {
  label: string;
  url: string;
  queryParams : any;
}

@Injectable({
  providedIn: 'root'
})
export class BreadcrumbService {
  private _breadcrumbs$ = new BehaviorSubject<Breadcrumb[]>([]);
  breadcrumbs$ = this._breadcrumbs$.asObservable();

  constructor(private router: Router) {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        const root = this.router.routerState.snapshot.root;
        const breadcrumbs: Breadcrumb[] = this.buildBreadcrumbs(root);
        this._breadcrumbs$.next(breadcrumbs);
        console.log(breadcrumbs);
      });
  }

  private buildBreadcrumbs(
    route: ActivatedRouteSnapshot,
    url: string = '',
    breadcrumbs: Breadcrumb[] = []
  ): Breadcrumb[] {
    if (route.routeConfig?.path) {
      const label = route.routeConfig.title?.toString() || this.formatLabel(route.routeConfig.path);

      url += `/${route.url.map(segment => segment.path.startsWith(':')?null : segment.path).join('/')}`;

      var params = route.params['id']
      breadcrumbs.push({ label, url ,queryParams:params});
    }

    if (route.firstChild) {
      return this.buildBreadcrumbs(route.firstChild, url, breadcrumbs);
    }

    return breadcrumbs;
  }
  
  private formatLabel(path: string): string {
    return path
      .split('/')
      .map(part => part.replace(/-/g, ' '))
      .map(part => part.charAt(0).toUpperCase() + part.slice(1))
      .join(' / ');
  }
}
