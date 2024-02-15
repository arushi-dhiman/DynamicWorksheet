//export class DynamicTabs {
//  TabId: number;
//  TabName: string;
//  Forms: DynamicForm[];

//  constructor() {
//    this.TabId = 0;
//    this.TabName = '';
//    this.Forms = [];
//  }
//}


//export class DynamicForm {
//  FormId: number;
//  FormName: string;
//  Fields: Record[];
//  constructor() {
//    this.FormId = 0;
//    this.FormName = '';
//    this.Fields = [];
//  }
//}
export class SaveTemplateDataModel {
  IsClear: boolean;
  UserId: number;
  TemplateId: number;
  TemplateFields: Record[];
  constructor() {
    this.IsClear = false;
    this.UserId = 0;
    this.TemplateId = 0;
    this.TemplateFields = [];
    

  }
}
export class Record {
  Id: number;
  UserId: number;
  FieldId: number;
  FieldValue: string;
  IsMultipleValues: boolean;

  constructor() {
    this.Id = 0;
    this.UserId = 1;
    this.FieldId = 0;
    this.FieldValue = null;
    this.IsMultipleValues = false;
  }
}
