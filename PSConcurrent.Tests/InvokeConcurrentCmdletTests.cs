﻿/*
    Copyright (C) 2017 Jeffrey Sharp

    Permission to use, copy, modify, and distribute this software for any
    purpose with or without fee is hereby granted, provided that the above
    copyright notice and this permission notice appear in all copies.

    THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
    WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
    MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
    ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
    WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
    ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
    OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
*/

using FluentAssertions;
using NUnit.Framework;

namespace PSConcurrent.Tests
{
    [TestFixture]
    public class InvokeConcurrentCmdletTests : CmdletTests
    {
        [Test]
        public void OneScriptWithOutput()
        {
            var output = Invoke(@"
                Invoke-Concurrent { 42 }
            ");

            output.Should().HaveCount(1);

            output.OfWorker(1).Should().Contain(42);
        }

        [Test]
        public void MultiScriptWithOutput()
        {
            var output = Invoke(@"
                Invoke-Concurrent { 42 }, { 123 }, { 31337 }
            ");

            output.Should().HaveCount(3);

            output.OfWorker(1).Should().Contain(   42);
            output.OfWorker(2).Should().Contain(  123);
            output.OfWorker(3).Should().Contain(31337);
        }
    }
}