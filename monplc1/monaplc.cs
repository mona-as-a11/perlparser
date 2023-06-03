
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using monplc1;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF              =  0, // (EOF)
        SYMBOL_ERROR            =  1, // (Error)
        SYMBOL_WHITESPACE       =  2, // Whitespace
        SYMBOL_MINUS            =  3, // '-'
        SYMBOL_MINUSMINUS       =  4, // '--'
        SYMBOL_EXCLAMEQ         =  5, // '!='
        SYMBOL_PERCENT          =  6, // '%'
        SYMBOL_LPAREN           =  7, // '('
        SYMBOL_RPAREN           =  8, // ')'
        SYMBOL_TIMES            =  9, // '*'
        SYMBOL_TIMESTIMES       = 10, // '**'
        SYMBOL_COMMA            = 11, // ','
        SYMBOL_DIV              = 12, // '/'
        SYMBOL_COLON            = 13, // ':'
        SYMBOL_SEMI             = 14, // ';'
        SYMBOL_LBRACE           = 15, // '{'
        SYMBOL_RBRACE           = 16, // '}'
        SYMBOL_PLUS             = 17, // '+'
        SYMBOL_PLUSPLUS         = 18, // '++'
        SYMBOL_LT               = 19, // '<'
        SYMBOL_EQ               = 20, // '='
        SYMBOL_EQEQ             = 21, // '=='
        SYMBOL_GT               = 22, // '>'
        SYMBOL_CASE             = 23, // case
        SYMBOL_DIGIT            = 24, // Digit
        SYMBOL_ELSE             = 25, // else
        SYMBOL_ELSIF            = 26, // elsif
        SYMBOL_FOR              = 27, // for
        SYMBOL_FOREACH          = 28, // foreach
        SYMBOL_ID               = 29, // Id
        SYMBOL_IF               = 30, // if
        SYMBOL_IN               = 31, // in
        SYMBOL_MY               = 32, // my
        SYMBOL_NEXT             = 33, // next
        SYMBOL_SUB              = 34, // sub
        SYMBOL_SWITCH           = 35, // switch
        SYMBOL_ASSIG            = 36, // <assig>
        SYMBOL_BODY             = 37, // <body>
        SYMBOL_CALL_METHOD      = 38, // <call_method>
        SYMBOL_CASE2            = 39, // <case>
        SYMBOL_CODE             = 40, // <code>
        SYMBOL_COND             = 41, // <cond>
        SYMBOL_DECINC_STMT      = 42, // <dec inc_stmt>
        SYMBOL_DECLARATION      = 43, // <declaration>
        SYMBOL_DIGIT2           = 44, // <digit>
        SYMBOL_EXP              = 45, // <exp>
        SYMBOL_EXPRESSION       = 46, // <expression>
        SYMBOL_FACTOR           = 47, // <factor>
        SYMBOL_FOR_STATMENT     = 48, // <for_statment>
        SYMBOL_FOREACH_STATMENT = 49, // <foreach_statment>
        SYMBOL_ID2              = 50, // <id>
        SYMBOL_IF_STATMENT      = 51, // <if_statment>
        SYMBOL_INITIAL          = 52, // <initial>
        SYMBOL_LIST             = 53, // <list>
        SYMBOL_METHOD           = 54, // <method>
        SYMBOL_NAME_METHOD      = 55, // <name_method>
        SYMBOL_OPERATION        = 56, // <operation>
        SYMBOL_PERLLANGUAGE     = 57, // <perl language>
        SYMBOL_STATMENT         = 58, // <statment>
        SYMBOL_SWITCH_STATMENT  = 59, // <switch_statment>
        SYMBOL_TERM             = 60, // <term>
        SYMBOL_WHILE_STATMENT   = 61  // <while_statment>
    };

    enum RuleConstants : int
    {
        RULE_PERLLANGUAGE_LBRACE_RBRACE                                                                                                        =  0, // <perl language> ::= '{' <code> '}'
        RULE_CODE                                                                                                                              =  1, // <code> ::= <statment>
        RULE_CODE2                                                                                                                             =  2, // <code> ::= <statment> <code>
        RULE_STATMENT                                                                                                                          =  3, // <statment> ::= <declaration>
        RULE_STATMENT2                                                                                                                         =  4, // <statment> ::= <assig>
        RULE_STATMENT3                                                                                                                         =  5, // <statment> ::= <if_statment>
        RULE_STATMENT4                                                                                                                         =  6, // <statment> ::= <for_statment>
        RULE_STATMENT5                                                                                                                         =  7, // <statment> ::= <foreach_statment>
        RULE_STATMENT6                                                                                                                         =  8, // <statment> ::= <while_statment>
        RULE_STATMENT7                                                                                                                         =  9, // <statment> ::= <switch_statment>
        RULE_STATMENT8                                                                                                                         = 10, // <statment> ::= <method>
        RULE_STATMENT9                                                                                                                         = 11, // <statment> ::= <call_method>
        RULE_DECLARATION_MY_SEMI                                                                                                               = 12, // <declaration> ::= my <id> ';'
        RULE_DECLARATION_MY_EQ_SEMI                                                                                                            = 13, // <declaration> ::= my <id> '=' <expression> ';'
        RULE_ASSIG_EQ_SEMI                                                                                                                     = 14, // <assig> ::= <id> '=' <expression> ';'
        RULE_ID_ID                                                                                                                             = 15, // <id> ::= Id
        RULE_EXPRESSION_PLUS                                                                                                                   = 16, // <expression> ::= <expression> '+' <term>
        RULE_EXPRESSION_MINUS                                                                                                                  = 17, // <expression> ::= <expression> '-' <term>
        RULE_EXPRESSION                                                                                                                        = 18, // <expression> ::= <term>
        RULE_TERM_TIMES                                                                                                                        = 19, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                                                                                                          = 20, // <term> ::= <term> '/' <factor>
        RULE_TERM_PERCENT                                                                                                                      = 21, // <term> ::= <term> '%' <factor>
        RULE_TERM                                                                                                                              = 22, // <term> ::= <factor>
        RULE_FACTOR_TIMESTIMES                                                                                                                 = 23, // <factor> ::= <factor> '**' <exp>
        RULE_FACTOR                                                                                                                            = 24, // <factor> ::= <exp>
        RULE_EXP_LPAREN_RPAREN                                                                                                                 = 25, // <exp> ::= '(' <expression> ')'
        RULE_EXP                                                                                                                               = 26, // <exp> ::= <digit>
        RULE_EXP2                                                                                                                              = 27, // <exp> ::= <id>
        RULE_DIGIT_DIGIT                                                                                                                       = 28, // <digit> ::= Digit
        RULE_IF_STATMENT_IF_LPAREN_RPAREN_LBRACE_RBRACE                                                                                        = 29, // <if_statment> ::= if '(' <cond> ')' '{' <code> '}'
        RULE_IF_STATMENT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ELSE_LBRACE_RBRACE                                                                     = 30, // <if_statment> ::= if '(' <cond> ')' '{' <code> '}' else '{' <code> '}'
        RULE_IF_STATMENT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ELSIF_LPAREN_RPAREN_LBRACE_RBRACE_ELSIF_LPAREN_RPAREN_LBRACE_RBRACE_ELSE_LBRACE_RBRACE = 31, // <if_statment> ::= if '(' <cond> ')' '{' <code> '}' elsif '(' <cond> ')' '{' <code> '}' elsif '(' <cond> ')' '{' <code> '}' else '{' <code> '}'
        RULE_COND                                                                                                                              = 32, // <cond> ::= <expression> <operation> <expression>
        RULE_OPERATION_GT                                                                                                                      = 33, // <operation> ::= '>'
        RULE_OPERATION_LT                                                                                                                      = 34, // <operation> ::= '<'
        RULE_OPERATION_EQEQ                                                                                                                    = 35, // <operation> ::= '=='
        RULE_OPERATION_EXCLAMEQ                                                                                                                = 36, // <operation> ::= '!='
        RULE_FOR_STATMENT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE                                                                            = 37, // <for_statment> ::= for '(' <initial> ';' <cond> ';' <dec inc_stmt> ')' '{' <code> '}'
        RULE_FOR_STATMENT_FOR_IN                                                                                                               = 38, // <for_statment> ::= for <id> in <id>
        RULE_FOR_STATMENT_FOR_EQ_COLON                                                                                                         = 39, // <for_statment> ::= for <id> '=' <digit> ':' <digit>
        RULE_INITIAL                                                                                                                           = 40, // <initial> ::= <assig>
        RULE_INITIAL2                                                                                                                          = 41, // <initial> ::= <declaration>
        RULE_DECINC_STMT_PLUSPLUS                                                                                                              = 42, // <dec inc_stmt> ::= '++' <id>
        RULE_DECINC_STMT_MINUSMINUS                                                                                                            = 43, // <dec inc_stmt> ::= '--' <id>
        RULE_DECINC_STMT_PLUSPLUS2                                                                                                             = 44, // <dec inc_stmt> ::= <id> '++'
        RULE_DECINC_STMT_MINUSMINUS2                                                                                                           = 45, // <dec inc_stmt> ::= <id> '--'
        RULE_FOREACH_STATMENT_FOREACH_LPAREN_RPAREN_LBRACE_RBRACE                                                                              = 46, // <foreach_statment> ::= foreach <id> '(' <list> ')' '{' <code> '}'
        RULE_LIST                                                                                                                              = 47, // <list> ::= <id>
        RULE_LIST_COMMA                                                                                                                        = 48, // <list> ::= <id> ',' <list>
        RULE_WHILE_STATMENT_LPAREN_RPAREN_LBRACE_RBRACE                                                                                        = 49, // <while_statment> ::= '(' <cond> ')' '{' <code> '}'
        RULE_SWITCH_STATMENT_SWITCH_LPAREN_RPAREN_LBRACE_RBRACE_ELSE_LBRACE_RBRACE_RBRACE                                                      = 50, // <switch_statment> ::= switch '(' <id> ')' '{' <case> '}' else '{' <code> '}' '}'
        RULE_CASE_CASE_LBRACE_NEXT_SEMI_RBRACE                                                                                                 = 51, // <case> ::= case <id> '{' <code> next ';' '}'
        RULE_CASE_CASE_LBRACE_NEXT_SEMI_RBRACE2                                                                                                = 52, // <case> ::= case <id> '{' <code> next ';' '}' <case>
        RULE_METHOD_SUB_LBRACE_RBRACE                                                                                                          = 53, // <method> ::= sub <name_method> '{' <body> '}'
        RULE_BODY                                                                                                                              = 54, // <body> ::= <declaration>
        RULE_BODY2                                                                                                                             = 55, // <body> ::= <declaration> <body>
        RULE_NAME_METHOD                                                                                                                       = 56, // <name_method> ::= <id>
        RULE_NAME_METHOD2                                                                                                                      = 57, // <name_method> ::= <digit>
        RULE_CALL_METHOD_SUB_LBRACE_RBRACE                                                                                                     = 58  // <call_method> ::= sub <name_method> '{' <declaration> '}'
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox list;

        public MyParser(string filename,ListBox list)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                             
                                               FileShare.Read);
            this.list = list;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSIF :
                //elsif
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOREACH :
                //foreach
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //Id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IN :
                //in
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MY :
                //my
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NEXT :
                //next
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SUB :
                //sub
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIG :
                //<assig>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BODY :
                //<body>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CALL_METHOD :
                //<call_method>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE2 :
                //<case>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CODE :
                //<code>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECINC_STMT :
                //<dec inc_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECLARATION :
                //<declaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_STATMENT :
                //<for_statment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOREACH_STATMENT :
                //<foreach_statment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STATMENT :
                //<if_statment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INITIAL :
                //<initial>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LIST :
                //<list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHOD :
                //<method>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NAME_METHOD :
                //<name_method>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OPERATION :
                //<operation>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERLLANGUAGE :
                //<perl language>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATMENT :
                //<statment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH_STATMENT :
                //<switch_statment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE_STATMENT :
                //<while_statment>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PERLLANGUAGE_LBRACE_RBRACE :
                //<perl language> ::= '{' <code> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CODE :
                //<code> ::= <statment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CODE2 :
                //<code> ::= <statment> <code>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT :
                //<statment> ::= <declaration>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT2 :
                //<statment> ::= <assig>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT3 :
                //<statment> ::= <if_statment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT4 :
                //<statment> ::= <for_statment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT5 :
                //<statment> ::= <foreach_statment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT6 :
                //<statment> ::= <while_statment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT7 :
                //<statment> ::= <switch_statment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT8 :
                //<statment> ::= <method>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT9 :
                //<statment> ::= <call_method>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARATION_MY_SEMI :
                //<declaration> ::= my <id> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARATION_MY_EQ_SEMI :
                //<declaration> ::= my <id> '=' <expression> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIG_EQ_SEMI :
                //<assig> ::= <id> '=' <expression> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= Id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_PLUS :
                //<expression> ::= <expression> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_MINUS :
                //<expression> ::= <expression> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<expression> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_PERCENT :
                //<term> ::= <term> '%' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TIMESTIMES :
                //<factor> ::= <factor> '**' <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_LPAREN_RPAREN :
                //<exp> ::= '(' <expression> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP2 :
                //<exp> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STATMENT_IF_LPAREN_RPAREN_LBRACE_RBRACE :
                //<if_statment> ::= if '(' <cond> ')' '{' <code> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STATMENT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ELSE_LBRACE_RBRACE :
                //<if_statment> ::= if '(' <cond> ')' '{' <code> '}' else '{' <code> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STATMENT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ELSIF_LPAREN_RPAREN_LBRACE_RBRACE_ELSIF_LPAREN_RPAREN_LBRACE_RBRACE_ELSE_LBRACE_RBRACE :
                //<if_statment> ::= if '(' <cond> ')' '{' <code> '}' elsif '(' <cond> ')' '{' <code> '}' elsif '(' <cond> ')' '{' <code> '}' else '{' <code> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <expression> <operation> <expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_GT :
                //<operation> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_LT :
                //<operation> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_EQEQ :
                //<operation> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_EXCLAMEQ :
                //<operation> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STATMENT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE :
                //<for_statment> ::= for '(' <initial> ';' <cond> ';' <dec inc_stmt> ')' '{' <code> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STATMENT_FOR_IN :
                //<for_statment> ::= for <id> in <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STATMENT_FOR_EQ_COLON :
                //<for_statment> ::= for <id> '=' <digit> ':' <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_INITIAL :
                //<initial> ::= <assig>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_INITIAL2 :
                //<initial> ::= <declaration>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECINC_STMT_PLUSPLUS :
                //<dec inc_stmt> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECINC_STMT_MINUSMINUS :
                //<dec inc_stmt> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECINC_STMT_PLUSPLUS2 :
                //<dec inc_stmt> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECINC_STMT_MINUSMINUS2 :
                //<dec inc_stmt> ::= <id> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOREACH_STATMENT_FOREACH_LPAREN_RPAREN_LBRACE_RBRACE :
                //<foreach_statment> ::= foreach <id> '(' <list> ')' '{' <code> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LIST :
                //<list> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LIST_COMMA :
                //<list> ::= <id> ',' <list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILE_STATMENT_LPAREN_RPAREN_LBRACE_RBRACE :
                //<while_statment> ::= '(' <cond> ')' '{' <code> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_STATMENT_SWITCH_LPAREN_RPAREN_LBRACE_RBRACE_ELSE_LBRACE_RBRACE_RBRACE :
                //<switch_statment> ::= switch '(' <id> ')' '{' <case> '}' else '{' <code> '}' '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_CASE_LBRACE_NEXT_SEMI_RBRACE :
                //<case> ::= case <id> '{' <code> next ';' '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_CASE_LBRACE_NEXT_SEMI_RBRACE2 :
                //<case> ::= case <id> '{' <code> next ';' '}' <case>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHOD_SUB_LBRACE_RBRACE :
                //<method> ::= sub <name_method> '{' <body> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BODY :
                //<body> ::= <declaration>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BODY2 :
                //<body> ::= <declaration> <body>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NAME_METHOD :
                //<name_method> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NAME_METHOD2 :
                //<name_method> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CALL_METHOD_SUB_LBRACE_RBRACE :
                //<call_method> ::= sub <name_method> '{' <declaration> '}'
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '" + args.UnexpectedToken.ToString() + "'" + " in line: " + args.UnexpectedToken.Location.LineNr;
            list.Items.Add(message);
            String message2 = "Expected Token is: " + args.ExpectedTokens.ToString();
            list.Items.Add(message2);
            //todo: Report message to UI?
        }

    }
}
 