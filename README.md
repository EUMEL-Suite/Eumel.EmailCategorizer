# Eumel Email-Categorizer

The EUMEL email categorizer is an outlook add-in to tag or categorize email in its subject. The goal is sending an email with a prefixed subject to categorize emails before it is sent.

A detailed documentation can be found in at [organization page](https://eumel-suite.github.io/pages/emailcategorizer.html).

# Basic usage

The email is written regilarily and can contain a category in brackets "[]". The subject is parsed for the category and added to the front of the subject.

![Subject Email](/Assets/eumel_mailsource.png?raw=true)

After sending the email, a dialog is show which presents the category and the subject.

![Subject Editor](/Assets/eumel_subjecteditor.png?raw=true)

# Configuration

The configuration is put into a "backstage view" which cna be found underneath the "File" menu.

![Configuration Entry](/Assets/eumel_configurationoverview.png?raw=true)

The first option provides a dialog to edit the categories, which are stored during sending emails.

![Categories Editor](/Assets/eumel_categoryeditor.png?raw=true)

The second option provides a dialog to edit the settings

![Settings Editor](/Assets/eumel_editsettings.png?raw=true)

