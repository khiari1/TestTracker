import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'tsi-spinner',
  template: `
  <div class="flex align-items-center">
  <div class="loader">
</div>
  <span class="pl-1 text-blue-600" *ngIf="label">{{label}}</span>
  </div>
      
  `,
})
export class SpinnerComponent implements OnInit {
  @Input() label: string | undefined;
  ngOnInit(): void {}
}
