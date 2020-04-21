# Hello Formik (and friends)

<!-- Id: formik  -->
<!-- Categories: React, Formik, Yup -->
<!-- Date: 20200409  -->

<!-- #header -->
As React is library and not aspire by any means to be full fledged framework, there is no support for creating forms. React delegates this responsibility to third party libraries. 
Among couple of libraries supporting forms development in React, ```Formik``` seems to be most popular solution.
<!-- #endheader -->


I decided to develop [Contact](/contact) page to my blog engine to introduce some way of communication between me and readers.

```
npm install formik --save
```

_Formik_ source code is written in _Typescript_ so there's no need to explicitly install something like ```@types\formik```.

Let's start with model data. It won't be representative as there are only text fields but at least this is _real case scenario_. 




```javascript
export interface ContactDataModel {
  username: string;
  email: string;
  subject: string;
  message: string;
}
```

I use [Material-UI](https://material-ui.com/) to have my components more or less in Material Design fashion, so I found library which allows to combine Formik and Material-UI - [material-ui-formik-components](https://www.npmjs.com/package/material-ui-formik-components).
There are no Typescript types provided but in this case they won't be necessary.

```
npm install --save material-ui-formik-components
```

To take advantage of Formik support for props of our model we need to wrap our model in ```FormikProps``` generic and use Typescript 
[Intersection Types](https://www.typescriptlang.org/docs/handbook/advanced-types.html#intersection-types) feature.

```javascript
class ContactPage extends Component<Props & FormikProps<ContactDataModel>>
{

} 
```

Where `Props` are other props not related to Formik that can be used in component.

In most simpliest way form will be defined as below:

```javascript
 <Formik
    initialValues={ this.initialValues}
 >
        {() => (
          
          <Form>
          <Field name="username" label="Username" component={TextField}  variant="filled" />
          <Field name="email" label="Email" component={TextField}  variant="filled"/>
          <Field name="subject" label="Subject" component={TextField}  variant="filled"/>
          <Field name="message" label="Message" multiline component={TextField}  variant="filled" rows={4}/>
          <Spacer height={20} />
          <FunkyButton buttonType="border" title="Submit" onClickEvent={() => {}} submit /> 
          </Form>

        )}
      </Formik>
```

That's only markups, nothing fancy about that. To see some of Formik features I need to add some other props to ```<Formik>``` component.

One of most important features of form development is form validation. In Formik, validation can be done in following fashion:

```javascript
<Formik
    validate={ (values: ContactDataModel) => { 
            
          let errors : FormikErrors<ContactDataModel> = {};

          if ( values.username === '') errors.username = 'Username is required';

          // (...)

          return errors;
        }}
>
```

```FormikErrors<T>``` creates object that have string properties with names corresponding to prop names in T class. In our example I got only ```string``` properties in model, but will it be ```age: number``` there will be ```errors.age: string``` and will be supported by Typescript.

This way of validation is simple to understand and develop but on the long run is rather clunky. Ex. for email validation I need to remember regex for email and regex implementation in javascript.

As usually there's library that will make our life easier, in this case this is [Yup](https://github.com/jquense/yup).
_Yup_ is independent tool, but _Formik_ author liked is so much that created property to accept _Yup_ validation engine.

#### Hello Yup

With _Yup_ we create validation mechanism.

```javascript
const validationSchema = yup.object().shape<ContactDataModel>({
    username: yup
      .string()
      .max(30)
      .required(),
    email: yup
      .string()
      .email()
      .required('Required!'),
    subject: yup
      .string()
      .max(30)
      .required('Really required!'),
    message: yup
      .string()
      .required()
  });
```

This definition must be bound to _Formik_ with this way

```javascript
   <Formik 
    // (...)
    validationSchema={validationSchema}  >
```


As we see in single object we define whole validation logic. ```.email()``` ensures in-built validation for email format. Every validation has default message which can be overriden. In example above all validations are declarative. Without Yup we use imperative programming which despite being clunky is more flexible and predictable.
On the other hand we cannot use ```validate``` and ```validationSchema``` together.
Luckilly Yup provides ```.test``` - great mechanism for imperative programming and also for cross-property validation. 
Suppose we want to validate if _Message_ is longer that _Subject_ and also that _Message_ does not contain bad language (I do not want to know if my blog is shit).

```javascript
// (...)
message: yup
      .string()
      .required('Required!')
      .test('message-test', 'Message should be longer than Subject', function(value) {
        let { subject } = this.parent;
        if (isNullOrUndefined(value) || isNullOrUndefined(subject)) return false;
        let isGreater = value.length > subject.length;
        return isGreater;
      })
      .test('message-test-bad-language', 'Message should not contain bad language', function(value) {
        if (isNullOrUndefined(value)) return true;
        if (value.includes('shit')) return false;
        return true;
      })
```

We can add as many test as we need although in default configuration only one will be displayed.

When data is valid it can be further processed.

```javascript
<Formik
/// (...)
onSubmit={this.onSubmit.bind(this)} >
```

in my case:

```javascript
onSubmit(data: ContactDataModel) {
    this.props.dispatch(postContactMessage(data));
}
```

##### Summary
I created very simple yet useful example using _Formik_ altogether with _Yup_. At first glance it seems to be very useful and gives motivation for further drilling. In this business devil is in details. I need to check more complicated controls with dynamic data bound and in general more complicated scenarios. But for that I will wait for case scenario in real world.
























