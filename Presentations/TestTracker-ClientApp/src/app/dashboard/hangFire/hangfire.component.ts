import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-hangfire',
  template: `
    
    <iframe [src]="hangfireDashboardUrl" width="100%" height="100%"></iframe>
  `,
})
export class HangfireComponent {
  constructor(private sanitizer: DomSanitizer, private http: HttpClient) {}

  hangfireDashboardUrl: SafeResourceUrl =
    this.sanitizer.bypassSecurityTrustResourceUrl(
      'https://localhost:7092/hangfire'
    );
}
