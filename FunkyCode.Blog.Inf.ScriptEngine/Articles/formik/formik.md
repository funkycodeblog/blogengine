# Hello Formik (and friends)

<!-- Id: formik  -->
<!-- Categories: React -->
<!-- Date: 20200409  -->

<!-- #header -->
As React is library and not aspire by any means to be full fledged framework, there is no support for creating forms. React delegates this responsibility to third party libraries. 
Among couple of libraries supporting forms development in React, Formik seems to be most popular solution.
<!-- #endheader -->


I decided to develop Contact Page to my blog engine to introduce some way of communication between me and readers of my blog.

```
npm install formik --save
```

Formik source code is written in TypeScript so there's no need to explicitly install something like ```@types\formik```.

Let's start with model data. It won't be representative as there are only text fields.

``` typescript
export interface ContactDataModel {
  username: string;
  email: string;
  subject: string;
  message: string;
}
```

I use [Material-UI](https://material-ui.com/) to have my components more or less in Material Design fashion, so I found library which allows to combine Formik and Material-UI [material-ui-formik-components](https://www.npmjs.com/package/material-ui-formik-components).
There are no Typescript types provided but in this case they won't be necessary.

```
npm install --save material-ui-formik-components
```

To take advantage of Formik support for props of our model we need to wrap our model in ```FormikProps``` generic and use Typescript 
[Intersection Types](https://www.typescriptlang.org/docs/handbook/advanced-types.html#intersection-types) feature.

```typescript
class ContactPage extends Component<Props & FormikProps<ContactDataModel>>
{

} 
```

Where `Props` are other props that can be used in component not related to Formik.

In most simpliest way form will be defined as below:

```typescript
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

That's only markups, nothing fancy about that. To see some of Formik features I need to add some other props.

One of most important features of form development is of course of validation. In Formik validation can be done in following fashion:

```typescript
<Formik
    validate={ (values: ContactDataModel) => { 
            
          let errors : FormikErrors<ContactDataModel> = {};

          if ( values.username === '') errors.username = 'Username is required';

          // (...)

          return errors;
        }}
>
```

```FormikErrors<T>``` creates object that have string properties with names corresponding to prop names in T class. In our example I got only ```string``` properties in model), but also for ```age: number``` there will be ```errors.age: string``` and will be supported by Typescript.

This way validation of validation is simple to understand and develop but on the long run is rather clunky. For email validation I need to remember regex or search something on web.

As usually there's liberary that will make our life easier, in this case this is [Yup](https://github.com/jquense/yup).
```Yup``` is independent tool, but Formik author liked is so much that created property to accept Yup validation engine.

#### Hello Yup

With Yup we create validation mechanism.

```typescript
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

This definition must be bound to Formik with this way

```typescript
   <Formik 
    // (...)
    validationSchema={validationSchema}  >
```


As we see in single object we define whole validation logic. ```.email()``` ensures in-built validation for email format. Every validation has default message which can be overriden. In example above all validations are declarative. Without Yup we use imperative programming which althout clunky is more flexible. 
On the other hand we cannot use ```validate``` and ```validationSchema``` together.
Luckilly Yup provides ```.test``` - great mechanism for imperative programming and also for cross-property validation. 
Suppose we want to validate if Message is longer that Subject and also that Message does not contain bad language (I do not want to know if my blog is shit :)).

```typescript
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

```typescript
<Formik
/// (...)
onSubmit={this.onSubmit.bind(this)} >
```

in my case:

```typescript
onSubmit(data: ContactDataModel) {
    this.props.dispatch(postContactMessage(data));
}
```

##### Summary
I created very simple yet useful example using Formik altogether with Yup. At first glance it seems to be very useful and gives motivation for further drilling. In this business devil is in details. I need to check more complicated controls with dynamic data bound and in general more complicated scenarios. But for that I will wait for case scenario in real world.
























