import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-unauthorized',
  templateUrl: './unauthorized.component.html',
  styleUrls: ['./unauthorized.component.css'],
  providers: [MessageService],
})
export class UnauthorizedComponent implements OnInit {
  previousUrl: string | null = null;
  navigationName: string | undefined;

  constructor(
    private messageService: MessageService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.navigationName = params['navigationName'] || 'Unknown Navigation';
    });
    this.messageService.add({
      severity: 'error',
      summary: 'Unauthorized',
      detail: 'You do not have the required permissions.',
    });
    this.previousUrl = sessionStorage.getItem('previousUrl');
  }
  goBack(): void {
    if (this.previousUrl) {
      window.history.back();
    }
  }
}
