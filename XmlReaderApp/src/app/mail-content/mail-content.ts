import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReadXmlService } from '../services/read-xml-service';
import { Product } from '../model/product';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-mail-content',
  imports: [
    FormsModule, CommonModule
],
  templateUrl: './mail-content.html',
  styleUrl: './mail-content.scss'
})
export class MailContent {
  content: string = 'THis is a test';
  result: Product | null = null;
  errorMessage:string | null = null;
  constructor(private xmlService: ReadXmlService, private toastr: ToastrService) {
    
  }

  clearContent() {
    console.log("Clearing content");
    this.content = '';
    this.result = null;
    this.errorMessage = null;
  }

  submitContent() {
    this.result = null;
    this.errorMessage = null;
    console.log("Submitted content:", this.content);
    this.xmlService.parseMailContent(this.content).subscribe(result => {
      this.result = result;
    }, error => {
      this.errorMessage = error.error || 'An error occurred while processing the XML.';
      this.result = null;
    });
  }
}
