


export interface TemplateModel {
  TemplateId: number;
  TemplateName: string;
  Tabs: TabModel[];

 
}


export interface TabModel {
  TabId: number;
  TabName: string;
  FormData: FormDataModel[];
}

export interface FormDataModel {
  FormId : number;
  FormHeading: string;
  FormName: string;
  Fields: FieldModel[] ;
}

export interface FieldModel {
  FieldId : number;
  Label: string;
  FieldName: string;
  FieldType: string;
  Options : OptionModel[]
}
export interface OptionModel{
  Key: number;
  Value : string;
}